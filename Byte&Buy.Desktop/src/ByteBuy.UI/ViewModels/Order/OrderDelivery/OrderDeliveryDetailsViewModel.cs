using ByteBuy.Services.DTO.OrderDelivery;
using ByteBuy.UI.ViewModels.Shared;
using System;

namespace ByteBuy.UI.ViewModels.Order.OrderDelivery;

public abstract class OrderDeliveryDetailsViewModel
{
    public string DeliveryName { get; }
    public string CarrierCode { get; }
    public MoneyViewModel Price { get; }

    protected OrderDeliveryDetailsViewModel(OrderDeliveryDetails dto)
    {
        DeliveryName = dto.DeliveryName;
        CarrierCode = dto.CarrierCode;
        Price = new MoneyViewModel(dto.Price);
    }

    public static OrderDeliveryDetailsViewModel From(OrderDeliveryDetails dto) =>
        dto switch
        {
            CourierDeliveryDetails courier => new CourierDeliveryViewModel(courier),
            PickupPointDeliveryDetails pickup => new PickupPointDeliveryViewModel(pickup),
            ParcelLockerDeliveryDetails parcel => new ParcelLockerDeliveryViewModel(parcel),
            _ => throw new NotSupportedException()
        };
}
