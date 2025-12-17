namespace ByteBuy.Core.DTO.DeliveryCarrier;

public record DeliveryCarrierResponse(
    Guid Id,
    string Name,
    string Code
);