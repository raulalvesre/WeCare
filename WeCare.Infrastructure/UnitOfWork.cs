namespace WeCare.Infrastructure;

public class UnitOfWork
{
    private readonly WeCareDatabaseContext _database;

    public UnitOfWork(WeCareDatabaseContext database)
    {
        _database = database;
    }

    public async Task SaveAsync()
    {
        await _database.SaveChangesAsync();
    }
}