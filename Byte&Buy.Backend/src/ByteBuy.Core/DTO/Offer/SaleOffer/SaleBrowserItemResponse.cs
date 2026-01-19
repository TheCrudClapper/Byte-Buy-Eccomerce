using ByteBuy.Core.DTO.Money;
using ByteBuy.Core.DTO.Offer.Common;

namespace ByteBuy.Core.DTO.Offer.SaleOffer;

public record SaleBrowserItemResponse : OfferBrowserItemResponse
{
    public MoneyDto PricePerItem { get; init; } = null!;
}
