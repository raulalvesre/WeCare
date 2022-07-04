using Microsoft.EntityFrameworkCore;
using WeCare.Domain;

namespace WeCare.Infrastructure;

public class DatabaseContext : DbContext
{
    
    private static readonly string _connStr = @"
        Host=localhost;
        Port=5432;
        Database=wecare;
        User Id=postgres;
        Password=rar432;
    ";
    
    public DbSet<Candidate> Candidates { get; set; } = null!;
    public DbSet<Institution> Institutions  { get; set; } = null!;
    public DbSet<Address> Addresses  { get; set; } = null!;

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