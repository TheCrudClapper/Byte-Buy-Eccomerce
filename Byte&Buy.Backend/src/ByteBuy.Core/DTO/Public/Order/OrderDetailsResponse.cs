using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.DTO.Public.OrderDelivery;
using ByteBuy.Core.DTO.Public.PortalUser;

namespace ByteBuy.Core.DTO.Public.Order;

public sealed record OrderDetailsResponse(
    Guid Id,
    Guid? PaymentId,
    OrderStatus Status,
    DateTime PurchasedDate,
    DateTime? DateDelivered,
    string SellerDisplayName,
    int LinesCount,
    MoneyDto TotalItemsCost,
    MoneyDto TotalCost,
    OrderDeliveryDetails DeliveryDetails,
    BuyerSnapshotResponse BuyerSnapshot,
    IReadOnlyCollection<UserOrderLineResponse> Lines);
