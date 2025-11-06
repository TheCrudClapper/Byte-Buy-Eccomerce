using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class RentOffer : Offer
{
    public Money PricePerDay { get; set; } = null!;
    public int MaxRentalDays { get; set;  }
}
