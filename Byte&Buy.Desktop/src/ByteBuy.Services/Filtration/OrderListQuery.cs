namespace ByteBuy.Services.Filtration;

public sealed class OrderListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? BuyerFullName { get; init; }
    public string? BuyerEmail { get; init; }
    public DateTime? PurchasedFrom { get; init; }
    public DateTime? PurchasedTo { get; init; }
    public decimal? TotalFrom { get; init; }
    public decimal? TotalTo { get; init; }
}
