using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class SaleOffer : Offer
{ 
    public decimal PricePerItem { get; set; }
}
