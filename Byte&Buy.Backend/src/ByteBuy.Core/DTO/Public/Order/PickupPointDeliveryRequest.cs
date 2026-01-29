namespace ByteBuy.Core.DTO.Public.Order;

public record PickupPointDeliveryRequest(
    string PickupPointId,
    string Street,
    string City,
    string LocalNumber
    );