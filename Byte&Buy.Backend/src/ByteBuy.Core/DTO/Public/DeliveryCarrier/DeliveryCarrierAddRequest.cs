using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.DeliveryCarrier;

public record DeliveryCarrierAddRequest(
    [Required] string Name,
    [Required] string Code
);
