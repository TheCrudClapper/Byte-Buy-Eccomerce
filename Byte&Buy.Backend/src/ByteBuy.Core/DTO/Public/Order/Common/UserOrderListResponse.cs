using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Order.Common;

public record UserOrderListResponse(
    Guid OrderId,
    OrderStatus Status,
    DateTime PurchasedDate,
    string SellerDisplayName,
    int LinesCount,
    MoneyDto TotalItemsCost,
    MoneyDto DeliveryCost,
    MoneyDto TotalCost,
    IReadOnlyCollection<UserOrderLineResponse> Lines);
