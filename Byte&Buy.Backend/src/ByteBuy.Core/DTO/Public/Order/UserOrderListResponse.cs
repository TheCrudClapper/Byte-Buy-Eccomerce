using ByteBuy.Core.Domain.Orders.Enums;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order.OrderLine;

namespace ByteBuy.Core.DTO.Public.Order;

public record UserOrderListResponse(
    Guid OrderId,
    OrderStatus Status,
    DateTime PurchasedDate,
    string SellerDisplayName,
    string BuyerDisplayName,
    int LinesCount,
    MoneyDto TotalItemsCost,
    MoneyDto DeliveryCost,
    MoneyDto TotalCost,
    IReadOnlyCollection<UserOrderLineResponse> Lines);
