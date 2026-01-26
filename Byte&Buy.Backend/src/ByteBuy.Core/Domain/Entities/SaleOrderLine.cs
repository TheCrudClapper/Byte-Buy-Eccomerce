using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class SaleOrderLine : OrderLine
{
    public Money PricePerItem { get; init; } = null!;
}
