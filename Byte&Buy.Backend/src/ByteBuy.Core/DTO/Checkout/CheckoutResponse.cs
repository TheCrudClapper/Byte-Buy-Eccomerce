using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Checkout;

public record CheckoutResponse(
    string BuyerFirstName,
    string BuyerLastName,
    string BuyerPhone,
    IReadOnlyCollection<SellerGroup> SellersGroups,
    MoneyDto ItemsCost,
    MoneyDto Tax,
    MoneyDto TotalCost);
