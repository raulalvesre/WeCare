using Microsoft.EntityFrameworkCore;
using WeCare.Domain;

namespace WeCare.Infrastructure;

public class DatabaseContext : DbContext
{
    
    private static readonly string _connStr = @"
        Host=localhost;
        Port=7771;
        Database=wecare;
        User Id=postgres;
        Password=rar432;
    ";    //aqui n ne amigo
    
    public DbSet<User> User { get; set; }
    public DbSet<Candidate> Candidate { get; set; }
    public DbSet<Institution> Institution  { get; set; }

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