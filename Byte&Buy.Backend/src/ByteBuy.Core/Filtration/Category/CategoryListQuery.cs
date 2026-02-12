namespace ByteBuy.Core.Filtration.Category;

public sealed class CategoryListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? CategoryName { get; init; }
}
