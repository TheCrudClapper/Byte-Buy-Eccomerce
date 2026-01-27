using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Checkout;

public record CheckoutResponse(
    string BuyerFirstName,
    string BuyerLastName,
    string BuyerPhone,
    IReadOnlyCollection<SellerGroup> SellersGroups,
    MoneyDto ItemsCost,
    MoneyDto Tax,
    MoneyDto TotalCost);
