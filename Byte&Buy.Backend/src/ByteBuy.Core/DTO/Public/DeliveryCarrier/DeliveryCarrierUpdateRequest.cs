using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.DeliveryCarrier;

public record DeliveryCarrierUpdateRequest(
    [Required] string Name,
    [Required] string Code
);