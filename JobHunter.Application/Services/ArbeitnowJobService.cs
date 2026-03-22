using JobHunter.Application.Models;
using JobHunter.Domain.Entities;
using JobHunter.Domain.Enums;
using System.Net.Http.Json;

namespace JobHunter.Application.Services;
public class ArbeitnowJobService(HttpClient httpClient)
{
    public async Task<List<JobOpportunity>> GetDotNetJobsAsync()
    {
        var result = new List<JobOpportunity>();

        var response = await httpClient.GetFromJsonAsync<ArbeitnowResponse>("https://www.arbeitnow.com/api/job-board-api");

        if (response?.Data == null) return result;

        var dotNetJobs = response.Data.Where(j =>
            j.Title.Contains(".NET", StringComparison.OrdinalIgnoreCase) ||
            j.Title.Contains("C#", StringComparison.OrdinalIgnoreCase) ||
            j.Description.Contains("C#", StringComparison.OrdinalIgnoreCase));

        foreach (var job in dotNetJobs)
        {
            result.Add(new JobOpportunity
            {
                ExternalId = $"arbeitnow_{job.Slug}",
                Title = job.Title,
                Company = job.CompanyName,
                Url = job.Url,
                Description = job.Description,
                Status = JobStatus.New
            });
        }

        return result;
    }
}
