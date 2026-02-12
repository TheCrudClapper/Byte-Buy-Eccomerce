namespace ByteBuy.Services.Filtration;

public sealed class OrderListQuery
{
    public string? BuyerFullName { get; init; }
    public string? BuyerEmail { get; init; }
    public DateTime? PurchasedFrom { get; init; }
    public DateTime? PurchasedTo { get; init; }
    public decimal? TotalFrom { get; init; }
    public decimal? TotalTo { get; init; }
}
