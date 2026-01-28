namespace ByteBuy.Core.DTO.Public.Order;

// Dto that encapsulates delivery data per order from one seller.
public record SellerDeliveryRequest(
    Guid SellerId,
    Guid DeliveryId,

    // Only for courier
    Guid? ShippingAddressId,

    // Only for parcel lockers
    ParcelLockerDeliveryRequest? ParcelLockerData,

    // Only for pickup points
    PickupPointDeliveryRequest? PickupPointData
    );
