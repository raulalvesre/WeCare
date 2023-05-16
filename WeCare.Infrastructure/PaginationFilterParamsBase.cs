using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.SearchParams;
using WeCare.Domain.Core;

namespace WeCare.Infrastructure;

public abstract class PaginationFilterParamsBase<T> : IPaginationFilterParams<T> where T : class
{
    private Expression<Func<T, bool>> _predicate = PredicateBuilder.New<T>(true);
    private Func<IQueryable<T>, IQueryable<T>>? _preQuery;
    protected Expression<Func<T, object>>? OrderByExpression { get; set; }
    protected SortDirection? Direction { get; set; }

    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    public IQueryable<T> ApplyFilter(IQueryable<T> query)
    {
        Filter();
        query = query.AsNoTracking();

        if (_preQuery != null)
            query = _preQuery(query);

        if (SortDirection.Ascending.Equals(Direction))
        {
            query = query.AsExpandableEFCore().Where(_predicate).OrderBy(OrderByExpression);
        }
        else if (SortDirection.Descending.Equals(Direction))
        {
            query = query.AsExpandableEFCore().Where(_predicate).OrderByDescending(OrderByExpression);
        }
        else
        {
            query = query.AsExpandableEFCore().Where(_predicate);
        }
        
        return query;
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