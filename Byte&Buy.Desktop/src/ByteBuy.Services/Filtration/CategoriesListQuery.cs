namespace ByteBuy.Services.Filtration;

public sealed class CategoriesListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? CategoryName { get; init; }
}
