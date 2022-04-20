namespace WeCare.Infrastructure.Repositories.Base;

public abstract class GenericRepository<T> where T : class
{
    
    protected readonly DatabaseContext _database;
    
    public GenericRepository(DatabaseContext database)
    {
        _database = database;
    }

    public async Task Add(T record)
    {
        await _database.Set<T>().AddAsync(record);
    }

    public IQueryable<T> Query()
    {
        return _database.Set<T>().AsQueryable();
    }

    public Task Remove(T record)
    {
        _database.Set<T>().Remove(record);
        return Task.CompletedTask;
    }

    public Task Update(T record)
    {
        _database.Set<T>().Update(record);
        return Task.CompletedTask;
    }

}