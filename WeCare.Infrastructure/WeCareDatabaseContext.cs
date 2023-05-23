using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeCare.Domain;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure;

public class WeCareDatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Candidate> Candidates { get; set; } = null!;
    public DbSet<Institution> Institutions  { get; set; } = null!;
    public DbSet<ConfirmationToken> ConfirmationTokens { get; set; } = null!;
    public DbSet<VolunteerOpportunity> VolunteerOpportunities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               "Host=localhost;Port=5432;Database=wecare;User Id=postgres;Password=rar432;";

        optionsBuilder
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");
        builder.ApplyConfigurationsFromAssembly(typeof(WeCareDatabaseContext).Assembly);
    }
}