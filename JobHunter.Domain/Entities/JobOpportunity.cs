using JobHunter.Domain.Enums;

namespace JobHunter.Domain.Entities;

public class JobOpportunity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string ExternalId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string? SalaryRange { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool MentionedBlueCard { get; set; } = false;
    public JobStatus Status { get; set; } = JobStatus.New;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
