namespace WeCare.Domain;

public interface IPaginationFilterParams<T>
{
    int? PageNumber  { get; set; }
    int? PageSize { get; set; }
    IQueryable<T> ApplyFilter(IQueryable<T> query);
}