namespace WeCare.Domain;

public class Pagination<T>
{
    public int PageNumber  { get; set; }
    public int PageSize   { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => (PageNumber > 1);
    public bool HasNextPage => (PageNumber+1 < TotalPages);
    public IEnumerable<T> Data { get; set; }


    public Pagination(int pageNumber, int pageSize, int totalCount, int totalPages, IEnumerable<T> data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = totalPages;
        Data = data;
    }
}