namespace ByteBuy.Core.DTO.Delivery;

public record DeliveryAddRequest(
    string Name,
    string? Description,
    decimal Price
    );
