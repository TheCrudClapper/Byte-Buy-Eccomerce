namespace ByteBuy.Core.DTO.Public.Delivery;

public record DeliveryOptionResponse
    (Guid Id,
    string Name,
    string Carrier,
    string DeliveryChannel,
    decimal Amount,
    string Currency);
