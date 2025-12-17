using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Delivery;

public record DeliveryAddRequest(
    [Required, MaxLength(50)] string Name,
    [MaxLength(50)] string? Description,
    [Required] decimal Price,
    int? ParcelSizeId,
    [Required] int ChannelId
    );
