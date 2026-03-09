using System.ComponentModel;

namespace ByteBuy.Core.Domain.Deliveries.Enums;

public enum DeliveryChannel
{
    [Description("Courier")]
    Courier = 0,

    [Description("Parcel Locker")]
    ParcelLocker = 1,

    [Description("Pickup Point")]
    PickupPoint = 2,
}
