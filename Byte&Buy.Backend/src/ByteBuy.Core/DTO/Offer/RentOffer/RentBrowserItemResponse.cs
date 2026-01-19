using ByteBuy.Core.DTO.Money;
using ByteBuy.Core.DTO.Offer.Common;

namespace ByteBuy.Core.DTO.Offer.RentOffer;

//Dto for rent offer used in offer browser
public record RentBrowserItemResponse : OfferBrowserItemResponse
{
    public int MaxRentalDays { get; init; }
    public MoneyDto PricePerDay { get; init; } = null!;
}
