namespace ByteBuy.Core.DTO.Public.OrderDelivery;

public sealed record ParcelLockerDeliveryDetails(
    string ParcelLockerId) : OrderDeliveryDetails;
