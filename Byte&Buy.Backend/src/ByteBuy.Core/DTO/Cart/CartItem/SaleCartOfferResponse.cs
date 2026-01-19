using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Cart.CartItem;

public record SaleCartOfferResponse : CartOfferResponse
{
    public MoneyDto PricePerItem { get; init; } = null!;
}
