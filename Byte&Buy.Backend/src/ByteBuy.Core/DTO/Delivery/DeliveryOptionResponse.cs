using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.DTO.Delivery;

public record DeliveryOptionResponse
    (Guid Id,
    string Name,
    string Carrier,
    string DeliveryChannel,
    string PriceAndCurrency);
