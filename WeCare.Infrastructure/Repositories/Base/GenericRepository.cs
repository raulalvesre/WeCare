using Microsoft.EntityFrameworkCore;

namespace WeCare.Infrastructure.Repositories.Base;

public abstract class GenericRepository<T> where T : class
{
    protected readonly DatabaseContext _database;

    protected readonly DbSet<T> _set;

    public GenericRepository(DatabaseContext database)
    {
        _database = database;
        _set = database.Set<T>();

        if (_set == null)
            throw new InvalidOperationException($"Invalid DbSet Type: {typeof(T).Name}");
    }

    public virtual IQueryable<T> Query => _set.AsQueryable();

    public async Task Add(T record)
    {
        await _set.AddAsync(record);
    }

    public Task Update(T record)
    {
        _set.Update(record);
        return Task.CompletedTask;
    }

    public Task Remove(T record)
    {
        _set.Remove(record);
        return Task.CompletedTask;
    }
}