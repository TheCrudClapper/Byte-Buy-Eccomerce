using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.Extensions;

namespace ByteBuy.Core.Mappings;

public static class DeliveryMappings
{
    public static DeliveryResponse ToDeliveryResponse(this Delivery delivery)
        => new DeliveryResponse(
            delivery.Id,
            delivery.Name,
            delivery.Description,
            delivery.Price.Amount,
            delivery.Price.Currency,
            (int?)delivery.ParcelSize ?? null,
            (int)delivery.Channel,
            delivery.DeliveryCarrierId
            );

    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this Delivery delivery)
        => new SelectListItemResponse<Guid>(
            delivery.Id,
            string.Join(" ", delivery.Name, delivery.Price.Amount, delivery.Price.Currency)
            );

    public static DeliveryListResponse ToDeliveryListResponse(this Delivery delivery)
        => new DeliveryListResponse(
               delivery.Id,
               delivery.Name,
               delivery.Price.Currency,
               delivery.Price.Amount,
               delivery.DeliveryCarrier.Name
            );

    public static DeliveryOptionResponse ToDeliveryOptionResponse(this Delivery delivery)
        => new DeliveryOptionResponse(
            delivery.Id,
            delivery.Name,
            delivery.DeliveryCarrier.Name,
            delivery.Channel.GetDescription(),
            delivery.Price.Amount,
            delivery.Price.Currency
            );

    public static List<DeliveryOptionResponse> MapDeliveries(
    IEnumerable<Delivery> deliveries,
    DeliveryChannelEnum channel)
    {
        return deliveries
            .Where(d => d.Channel == channel)
            .OrderBy(d => d.Name)
            .Select(d => d.ToDeliveryOptionResponse())
            .ToList();
    }
}
