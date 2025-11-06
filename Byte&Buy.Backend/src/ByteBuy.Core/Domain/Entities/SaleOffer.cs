using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class SaleOffer : Offer
{
    public Money PricePerItem { get; set; } = null!;
}
