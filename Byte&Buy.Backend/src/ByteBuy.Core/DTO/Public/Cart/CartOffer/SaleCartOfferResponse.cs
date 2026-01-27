using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Cart.CartOffer;

public record SaleCartOfferResponse : CartOfferResponse
{
    public MoneyDto PricePerItem { get; init; } = null!;
}
