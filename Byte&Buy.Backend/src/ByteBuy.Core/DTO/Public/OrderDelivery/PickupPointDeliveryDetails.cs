namespace ByteBuy.Core.DTO.Public.OrderDelivery;

public sealed record PickupPointDeliveryDetails(
    string PickupPointName,
    string PickupPointId,
    string PickupStreet,
    string PickupCity,
    string PickupLocalNumber) : OrderDeliveryDetails;

