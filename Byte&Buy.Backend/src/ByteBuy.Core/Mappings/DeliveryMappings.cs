using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;

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
            (int)delivery.Channel
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
               delivery.Price.Amount
            );

}
