using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WeCare.Domain;

namespace WeCare.Infrastructure;

public abstract class PaginationFilterParamsBase<T> : IPaginationFilterParams<T> where T : class
{
    private Expression<Func<T, bool>> _predicate = PredicateBuilder.New<T>(true);
    private Func<IQueryable<T>, IQueryable<T>>? _preQuery;

    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    public IQueryable<T> ApplyFilter(IQueryable<T> query)
    {
        Filter();
        query = query.AsNoTracking();

        if (_preQuery is not null)
            query = _preQuery(query);

        return query.AsExpandableEFCore().Where(_predicate);
    }

    protected abstract void Filter();

    protected void And(Expression<Func<T, bool>> expression)
    {
        _predicate = _predicate.And(expression);
    }

    protected void Or(Expression<Func<T, bool>> expression)
    {
        _predicate = _predicate.Or(expression);
    }

    protected void PreQuery(Func<IQueryable<T>, IQueryable<T>> func)
    {
        _preQuery = func;
    }
}