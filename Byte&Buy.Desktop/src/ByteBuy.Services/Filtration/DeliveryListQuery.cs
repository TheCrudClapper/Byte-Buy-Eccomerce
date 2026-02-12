namespace ByteBuy.Services.Filtration;

public sealed class DeliveryListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? DeliveryName { get; init; }
    public decimal? PriceFrom { get; init; }
    public decimal? PriceTo { get; init; }
}
