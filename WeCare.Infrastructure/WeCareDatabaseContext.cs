using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeCare.Domain;

namespace WeCare.Infrastructure;

public class WeCareDatabaseContext : DbContext
{
    
    private static readonly string _connStr = @"
        Host=localhost;
        Port=5432;
        Database=postgres;
        User Id=postgres;
        Password=rar432;
    ";

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Candidate> Candidates { get; set; } = null!;
    public DbSet<Institution> Institutions  { get; set; } = null!;
    public DbSet<VolunteerOpportunity> VolunteerOpportunities { get; set; } = null!;
    public DbSet<ConfirmationToken> ConfirmationTokens { get; set; } = null;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connStr)
            .UseSnakeCaseNamingConvention()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");
        builder.ApplyConfigurationsFromAssembly(typeof(WeCareDatabaseContext).Assembly);
    }
}