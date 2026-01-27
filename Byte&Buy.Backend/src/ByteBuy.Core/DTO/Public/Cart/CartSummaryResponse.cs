using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Cart;

public record CartSummaryResponse(
    int ItemsQuantity,
    MoneyDto TotalItemsValue,
    MoneyDto TaxValue,
    MoneyDto EstimatedShippingCost,
    MoneyDto TotalCost);

