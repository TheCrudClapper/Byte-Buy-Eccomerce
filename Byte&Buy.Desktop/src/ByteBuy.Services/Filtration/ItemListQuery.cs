namespace ByteBuy.Services.Filtration;

public sealed class ItemListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public int? StockQuantityFrom { get; init; }
    public int? StockQuantityTo { get; init; }
    public string? Name { get; init; }
}
