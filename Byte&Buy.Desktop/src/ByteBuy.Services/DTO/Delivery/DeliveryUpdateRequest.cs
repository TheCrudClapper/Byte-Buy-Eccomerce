namespace ByteBuy.Services.DTO.Delivery;

public record DeliveryUpdateRequest(
    string Name,
    string? Description,
    decimal Price,
    int? ParcelSizeId,
    int ChannelId,
    Guid CarrierId
    );
