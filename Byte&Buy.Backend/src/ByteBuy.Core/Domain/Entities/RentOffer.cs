using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class RentOffer : Offer
{
    public decimal PricePerDay { get; set; }
    public int MaxRentalDays { get; set;  }
}
