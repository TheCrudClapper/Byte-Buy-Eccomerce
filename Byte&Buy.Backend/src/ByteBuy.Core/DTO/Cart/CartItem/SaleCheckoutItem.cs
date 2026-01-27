using ByteBuy.Core.DTO.Checkout;
using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Cart.CartItem;

public record SaleCheckoutItem : CheckoutItem
{
    MoneyDto PricePerItem { get; init; } = null!;
}
