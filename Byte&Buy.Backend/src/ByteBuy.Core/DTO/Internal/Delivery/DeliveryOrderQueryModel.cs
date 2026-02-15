using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.DTO.Internal.Delivery;

public sealed record DeliveryOrderQueryModel(
    Guid Id,
    string Name,
    string CarrierCode,
    DeliveryChannel Channel,
    decimal PriceAmount,
    string PriceCurrency);