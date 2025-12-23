using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using System.Linq.Expressions;

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

    public static List<DeliveryOptionResponse> MapDeliveries(
    IEnumerable<DeliveryOptionResponse> deliveries,
    DeliveryChannelEnum channel)
    {
        return deliveries
            .Where(d => d.DeliveryChannel == channel.ToString())
            .OrderBy(d => d.Name)
            .ToList();
    }

    //LINQ to SQL Projections
    public static Expression<Func<Delivery, DeliveryListResponse>> DeliveryListResponseProjection
        => d => new DeliveryListResponse(
            d.Id,
            d.Name,
            d.Price.Currency,
            d.Price.Amount,
            d.DeliveryCarrier.Name);

    public static Expression<Func<Delivery, SelectListItemResponse<Guid>>> DeliverySelectListProjection
        => d => new SelectListItemResponse<Guid>(d.Id, d.Name);

    public static Expression<Func<Delivery, DeliveryOptionResponse>> DeliveryOptionResponseProjection
        => d => new DeliveryOptionResponse(
            d.Id,
            d.Name,
            d.DeliveryCarrier.Name,
            d.Channel.ToString(),
            d.Price.Amount,
            d.Price.Currency);

}
