using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Delivery;

public record DeliveryAddRequest(
    [Required, MaxLength(50)] string Name,
    [MaxLength(50)] string? Description,
    [Required] decimal Price,
    int? ParcelSizeId,
    [Required] int ChannelId,
    [Required] Guid CarrierId
    );
