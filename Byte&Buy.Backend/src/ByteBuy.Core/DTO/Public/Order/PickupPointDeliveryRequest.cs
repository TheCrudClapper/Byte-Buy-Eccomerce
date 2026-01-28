namespace ByteBuy.Core.DTO.Public.Order;

public record PickupPointDeliveryRequest(
    Guid PickupPointId,
    string Street,
    string City,
    string LocalNumber
    );