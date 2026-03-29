using ByteBuy.Core.Domain.Orders.Enums;
using ByteBuy.Core.DTO.Public.Money;
namespace ByteBuy.Core.DTO.Internal.Order;

public sealed record UserOrderListQueryModel(
    Guid OrderId,
    OrderStatus Status,
    DateTime PurchasedDate,
    string SellerDisplayName,
    string BuyerDisplayName,
    int LinesCount,
    bool IsDeletable,
    MoneyDto TotalLinesCost,
    MoneyDto DeliveryCost,
    MoneyDto TotalCost,
    IReadOnlyCollection<UserOrderLineQueryModel> Lines
);