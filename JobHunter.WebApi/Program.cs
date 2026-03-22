using JobHunter.Application.Services;
using JobHunter.Application.Workers;
using JobHunter.Domain;
using JobHunter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NpgsqlConnection")));

builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddHostedService<JobSearchWorker>();
builder.Services.AddHttpClient<ArbeitnowJobService>();
builder.Services.AddHostedService<JobSearchWorker>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
