using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Checkout;

public record CheckoutResponse(
    string BuyerFirstName,
    string BuyerLastName,
    string BuyerPhone,
    IReadOnlyCollection<SellerGroup> SellersGroups,
    IReadOnlyCollection<SelectListItemResponse<int>> AvaliablePaymentMethods,
    MoneyDto ItemsCost,
    MoneyDto Tax,
    MoneyDto TotalCost,
    bool CanPlaceOrder);
