using JobHunter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobHunter.Infrastructure.Persistence;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<JobOpportunity> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JobOpportunity>()
            .HasIndex(j => j.ExternalId)
            .IsUnique();
    }
}
