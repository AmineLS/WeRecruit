using Microsoft.EntityFrameworkCore;
using WeRecruit.Entities;

namespace WeRecruit.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Submission> Submissions { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Submission>()
            .HasIndex(submission => submission.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}