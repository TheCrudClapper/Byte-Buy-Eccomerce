using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Cart.CartItem;

public record SaleCartOfferResponse : CartItemResponse
{
    public MoneyDto PricePerItem { get; init; } = null!;
}
