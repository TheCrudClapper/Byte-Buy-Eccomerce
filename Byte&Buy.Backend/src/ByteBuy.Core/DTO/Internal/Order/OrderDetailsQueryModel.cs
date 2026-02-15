using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.OrderDelivery;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Internal.Order;

public sealed record OrderDetailsQueryModel(
        Guid OrderId,
        //only when status == awaiting payment
        Guid? PaymentId,
        OrderStatus Status,
        DateTime PurchasedDate,
        DateTime? DateDelivered,
        string SellerDisplayName,
        int LinesCount,
        MoneyDto TotalLinesCost,
        MoneyDto TotalCost,
        OrderDeliveryQueryModel DeliveryQuery,
        BuyerSnapshotQueryModel BuyerDetailsQuery,
        IReadOnlyCollection<UserOrderLineQueryModel> Lines);