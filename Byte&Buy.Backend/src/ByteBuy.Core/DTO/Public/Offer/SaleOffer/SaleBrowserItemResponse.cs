using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Offer.Common;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleBrowserItemResponse : OfferBrowserItemResponse
{
    public MoneyDto PricePerItem { get; init; } = null!;
}
