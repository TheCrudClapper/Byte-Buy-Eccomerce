using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Offer.Common;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

//Dto for rent offer used in offer browser
public record RentBrowserItemResponse : OfferBrowserItemResponse
{
    public int MaxRentalDays { get; init; }
    public MoneyDto PricePerDay { get; init; } = null!;
}
