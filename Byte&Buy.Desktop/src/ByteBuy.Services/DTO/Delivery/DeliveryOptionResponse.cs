namespace ByteBuy.Services.DTO.Delivery;

public record DeliveryOptionResponse
    (Guid Id,
    string Name,
    string Carrier,
    string DeliveryChannel,
    decimal Amount,
    string Currency);
