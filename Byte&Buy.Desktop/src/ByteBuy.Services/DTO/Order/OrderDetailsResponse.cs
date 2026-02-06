using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Money;
using ByteBuy.Services.DTO.Order.Enums;
using ByteBuy.Services.DTO.Order.OrderLine;
using ByteBuy.Services.DTO.OrderDelivery;

namespace ByteBuy.Services.DTO.Order;
public sealed record BuyerSnapshotResponse(
     string FullName,
     string Email,
     string PhoneNumber,
     HomeAddressDto Address);

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
