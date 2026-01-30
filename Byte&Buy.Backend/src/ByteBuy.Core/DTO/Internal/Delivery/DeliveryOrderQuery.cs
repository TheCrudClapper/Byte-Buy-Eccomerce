using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.Delivery;

public record DeliveryOrderQuery(
    Guid Id,
    string Name, 
    string CarrierCode,
    DeliveryChannel channel,
    decimal priceAmount,
    string priceCurrency);