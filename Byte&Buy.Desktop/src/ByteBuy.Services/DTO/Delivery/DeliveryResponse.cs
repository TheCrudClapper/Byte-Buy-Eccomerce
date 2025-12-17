namespace ByteBuy.Core.DTO.Delivery;

public record DeliveryResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Amount,
    string Currency,
    int? ParcelSizeId,
    int ChannelId
    );
