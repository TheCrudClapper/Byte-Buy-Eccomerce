namespace ByteBuy.Services.Pagination;

public class PagedList<T>
{
    public IReadOnlyCollection<T> Items { get; set; } = [];
    public PaginationMetadata Metadata { get; set; } = new();
}
