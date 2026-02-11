using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Money;
namespace ByteBuy.Core.DTO.Internal.Order;

public sealed record UserOrderListQuery(
    Guid OrderId,
    OrderStatus Status,
    DateTime PurchasedDate,
    string SellerDisplayName,
    string BuyerDisplayName,
    int LinesCount,
    MoneyDto TotalLinesCost,
    MoneyDto DeliveryCost,
    MoneyDto TotalCost,
    IReadOnlyCollection<UserOrderLineQuery> Lines
);