using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Order.Enum;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order.Common;

namespace ByteBuy.Core.DTO.Internal.Order;

public sealed record UserOrderListQuery(
    Guid OrderId,
    OrderStatus Status,
    DateTime PurchasedDate,
    string SellerDisplayName,
    int LinesCount,
    MoneyDto TotalLinesCost,
    MoneyDto DeliveryCost,
    MoneyDto TotalCost,
    IReadOnlyCollection<UserOrderLineQuery> Lines
);