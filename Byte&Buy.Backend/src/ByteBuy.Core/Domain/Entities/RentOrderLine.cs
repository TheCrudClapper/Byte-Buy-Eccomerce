using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class RentOrderLine : OrderLine
{
    public Money PricePerDay { get; init; } = null!;
    public int RentalDays { get; init; }
}
