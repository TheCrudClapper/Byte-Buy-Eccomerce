using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Cart.CartOffer;

public record SaleCheckoutItem : CheckoutItem
{
    public MoneyDto PricePerItem { get; init; } = null!;
}
