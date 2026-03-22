using JobHunter.Application.Services;
using JobHunter.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace JobHunter.Application.Workers;
public class JobSearchWorker(ILogger<JobSearchWorker> logger,
                             IServiceProvider serviceProvider,
                             IConfiguration config) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Job Hunter Bot started!");

        var botToken = config["Telegram:BotToken"];
        var chatId = config["Telegram:ChatId"];

        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(chatId))
        {
            logger.LogError("Telegram credentials are missing in appsettings.json!");
            return;
        }

        var botClient = new TelegramBotClient(botToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Waking up to check for jobs at: {time}", DateTimeOffset.Now);

            using var scope = serviceProvider.CreateScope();
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var arbeitnowService = scope.ServiceProvider.GetRequiredService<ArbeitnowJobService>();

            var fetchedJobs = await arbeitnowService.GetDotNetJobsAsync();

            foreach (var job in fetchedJobs)
            {
                bool exists = await jobRepository.ExistsAsync(job.ExternalId);

                if (!exists)
                {
                    logger.LogInformation("Found new job: {Title} at {Company}", job.Title, job.Company);

                    await jobRepository.AddAsync(job);
                    await jobRepository.SaveChangesAsync();

                    string messageText = $"🚀 <b>New .NET Job Found!</b>\n\n" +
                     $"<b>Title:</b> {job.Title}\n" +
                     $"<b>Company:</b> {job.Company}\n\n" +
                     $"<b>Source:</b> Arbeitnow\n\n" +
                     $"<a href='{job.Url}'>Click here to view the job</a>";

                    try
                    {
                        await botClient.SendMessage(
                            chatId: chatId,
                            text: messageText,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                            cancellationToken: stoppingToken
                        );
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error sending Telegram message");
                    }
                }
            }

            await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
        }
    }
}
