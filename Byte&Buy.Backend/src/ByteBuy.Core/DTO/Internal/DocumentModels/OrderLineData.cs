using ByteBuy.Core.DTO.Internal.Order.Enum;
namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record OrderLineData
{
    public string ItemTitle { get; init; } = null!;
    public OrderLineType Type { get; init; }
    public int Quantity { get; init; }
    public int? RentalDays { get; init; }
    public string? PricePerDay { get; init; }
    public string? PricePerItem{ get; init; }
    public string Total { get; init; } = null!;
}
