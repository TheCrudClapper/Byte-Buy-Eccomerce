using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Checkout;

public record SellerGroup(
    Guid SellerId,
    string SellerDisplayName,
    string SellerEmail,
    MoneyDto ItemsWorth,
    IReadOnlyCollection<CheckoutItem> CheckoutItems);