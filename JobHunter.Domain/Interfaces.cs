using JobHunter.Domain.Entities;

namespace JobHunter.Domain;
public interface IJobRepository
{
    Task<bool> ExistsAsync(string externalId);
    Task AddAsync(JobOpportunity job);
    Task SaveChangesAsync();
}
