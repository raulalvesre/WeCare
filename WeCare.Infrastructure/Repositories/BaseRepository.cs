using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Domain.Core;

namespace WeCare.Infrastructure.Repositories.Base;

public abstract class BaseRepository<T> where T : class
{
    protected readonly WeCareDatabaseContext WeCareDatabaseContext;

    protected readonly DbSet<T> _set;

    public BaseRepository(WeCareDatabaseContext weCareDatabaseContext)
    {
        WeCareDatabaseContext = weCareDatabaseContext;
        _set = weCareDatabaseContext.Set<T>();

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
    
    public async Task<Pagination<T>> Paginate(IPaginationFilterParams<T> filterParams)
    {
        var recordList = new List<T>();
        var query = filterParams.ApplyFilter(WeCareDatabaseContext.Set<T>());
        
        int pageNumber = filterParams.PageNumber ?? 1;
        int pageSize = filterParams.PageSize ?? 10;
        int totalCount = await query.CountAsync();
        int totalPages = (int) Math.Ceiling(totalCount * 1.0 / pageSize);

        if (pageSize < 1)
            return new Pagination<T>(pageNumber, pageSize, totalCount, totalPages, recordList);

        int skip = (pageNumber - 1) * pageSize;
        
        recordList = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new Pagination<T>(pageNumber, pageSize, totalCount, totalPages, recordList);
    }
    
}