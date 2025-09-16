namespace Matorikkusu.Toolkit.Extensions;

public class PaginationResult<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public IEnumerable<T> Items { get; set; }

    public PaginationResult()
    {
        Items = [];
    }
}