namespace WeCare.Domain.Core;

public interface IPaginationFilterParams<T>
{
    int? PageNumber  { get; set; }
    int? PageSize { get; set; }
    IQueryable<T> ApplyFilter(IQueryable<T> query);
}