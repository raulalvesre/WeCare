using Microsoft.EntityFrameworkCore;
using WeCare.Domain;

namespace WeCare.Infrastructure;

public class DatabaseContext : DbContext
{
    
    public DbSet<Candidate> Candidates { get; set; }

    private static string _connStr = @"
        Server=127.0.0.1,5432;
        Database=WeCare;
        User Id=postgres;
        Password=rar432;
    ";
    
    public DatabaseContext() : base() {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connStr);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
    
}