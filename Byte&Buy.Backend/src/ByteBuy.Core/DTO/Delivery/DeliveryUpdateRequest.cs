using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Delivery;

public record DeliveryUpdateRequest(
    [Required, MaxLength(50)] string Name,
    [MaxLength(50)] string? Description,
    [Required] decimal Price
    );
