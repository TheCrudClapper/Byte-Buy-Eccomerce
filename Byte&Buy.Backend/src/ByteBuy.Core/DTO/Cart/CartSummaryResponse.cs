using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Cart;

public record CartSummaryResponse(
    int ItemsQuantity,
    MoneyDto TotalItemsValue,
    MoneyDto TaxValue,
    MoneyDto EstimatedShippingCost,
    MoneyDto TotalCost);

