namespace ByteBuy.Core.DTO.Delivery;

public record DeliveryUpdateRequest(
    string Name,
    string? Description,
    decimal Price
    );
