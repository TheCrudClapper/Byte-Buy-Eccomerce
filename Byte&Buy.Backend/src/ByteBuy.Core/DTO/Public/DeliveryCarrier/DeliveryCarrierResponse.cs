namespace ByteBuy.Core.DTO.Public.DeliveryCarrier;

public record DeliveryCarrierResponse(
    Guid Id,
    string Name,
    string Code
);