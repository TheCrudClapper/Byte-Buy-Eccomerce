using ByteBuy.Core.DTO.Internal.Order.Enum;
namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record OrderLineData
{
    public string ItemTitle { get; init; } = null!;
    public OrderLineType Type { get; init; }
    public int Quantity { get; init; }
    public int? RentalDays { get; init; }
    public decimal? PricePerDay { get; init; }
    public string? PricePerDayCurrency { get; init; }
    public decimal? PricePerItem{ get; init; }
    public string? PricePerItemCurrency{ get; init; }
    public decimal Total { get; init; }
    public string TotalCurrency { get; init; } = null!;
}
