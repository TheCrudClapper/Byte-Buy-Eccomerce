using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Checkout;

public record SellerGroup(
    Guid SellerId,
    string SellerDisplayName, 
    string SellerEmail,
    MoneyDto ItemsWorth,
    IReadOnlyCollection<CheckoutItem> CheckoutItems);