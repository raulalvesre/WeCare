using Microsoft.EntityFrameworkCore;
using WeCare.Domain;

namespace WeCare.Infrastructure;

public class DatabaseContext : DbContext
{
    
    private static readonly string _connStr = @"
        Server=127.0.0.1,5432;
        Database=WeCare;
        User Id=postgres;
        Password=rar432;
    ";

    public DbSet<Candidate> Candidate { get; set; }
    public DbSet<Institution> Institution { get; set; }
    public DbSet<InstitutionLineOfWork> InstitutionLineOfWork { get; set; }
    public DbSet<ParticipationCertificate> ParticipationCertificate { get; set; }
    public DbSet<VolunteerOpportunity> VolunteerOpportunity { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connStr)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");
        base.OnModelCreating(builder);
    }
    
}