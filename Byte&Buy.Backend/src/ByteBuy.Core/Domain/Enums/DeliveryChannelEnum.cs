using System.ComponentModel;

namespace ByteBuy.Core.Domain.Enums;

public enum DeliveryChannelEnum
{
    [Description("Courier")]
    Courier = 0,

    [Description("Parcel Locker")]
    ParcelLocker = 1,

    [Description("Pickup Point")]
    PickupPoint = 2,
}
