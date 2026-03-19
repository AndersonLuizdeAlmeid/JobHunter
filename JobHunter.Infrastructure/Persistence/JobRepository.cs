using JobHunter.Domain;
using JobHunter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobHunter.Infrastructure.Persistence;
public class JobRepository(AppDbContext context) : IJobRepository
{
    public async Task<bool> ExistsAsync(string externalId)
    {
        return await context.Jobs.AnyAsync(j => j.ExternalId == externalId);
    }

    public async Task AddAsync(JobOpportunity job)
    {
        await context.Jobs.AddAsync(job);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
