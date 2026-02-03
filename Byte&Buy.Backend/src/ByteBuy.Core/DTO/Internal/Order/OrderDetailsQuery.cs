using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.OrderDelivery;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Internal.Order;

public sealed record OrderDetailsQuery(
        Guid OrderId,
        OrderStatus Status,
        DateTime PurchasedDate,
        DateTime? DateDelivered,
        string SellerDisplayName,
        int LinesCount,
        MoneyDto TotalLinesCost,
        MoneyDto TotalCost,
        OrderDeliveryQuery DeliveryQuery,
        BuyerSnapshotQuery BuyerDetailsQuery,
        IReadOnlyCollection<UserOrderLineQuery> Lines);