using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Domain.Core;

namespace WeCare.Infrastructure.Repositories.Base;

public abstract class BaseRepository<T> where T : class
{
    protected readonly WeCareDatabaseContext WeCareDatabaseContext;

    protected readonly DbSet<T> _set;

    protected BaseRepository(WeCareDatabaseContext weCareDatabaseContext)
    {
        WeCareDatabaseContext = weCareDatabaseContext;
        _set = weCareDatabaseContext.Set<T>();

        if (_set == null)
            throw new InvalidOperationException($"Invalid DbSet Type: {typeof(T).Name}");
    }

    public virtual IQueryable<T> Query => _set.AsQueryable();

    public async Task Save(T record)
    {
        await _set.AddAsync(record);
    }

    public async Task SaveAll(IEnumerable<T> records)
    {
        await _set.AddRangeAsync(records);
    }

    public Task Update(T record)
    {
        _set.Update(record);
        return Task.CompletedTask;
    }

    public void UpdateAll(IEnumerable<T> records)
    {
        _set.UpdateRange(records);
    }

    public Task Remove(T record)
    {
        _set.Remove(record);
        return Task.CompletedTask;
    }
    
    public async Task<Pagination<T>> Paginate(IPaginationFilterParams<T> filterParams)
    {
        var recordList = new List<T>();
        var query = filterParams.ApplyFilter(WeCareDatabaseContext.Set<T>());
        
        int pageNumber = filterParams.PageNumber ?? 1;
        int pageSize = filterParams.PageSize ?? 10;
        int totalCount = await query.CountAsync();
        int totalPages = totalCount != 0 ? (int) Math.Ceiling(totalCount * 1.0 / pageSize) : 0;

        if (pageSize < 1)
            return new Pagination<T>(pageNumber, pageSize, totalCount, totalPages, recordList);

        int skip = (pageNumber - 1) * pageSize;
        
        recordList = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new Pagination<T>(pageNumber, pageSize, totalCount, totalPages, recordList);
    }

    public virtual ValueTask<T?> GetByIdAsync(long id)
    {
        return _set.FindAsync(id);
    }

}