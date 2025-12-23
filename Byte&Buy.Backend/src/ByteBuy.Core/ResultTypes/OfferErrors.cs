namespace ByteBuy.Core.ResultTypes;

public static class OfferErrors
{
    public static readonly Error DeliveryRequired
        = new Error(400, "At least one delivery is required");

    public static readonly Error MultipleParcelLockersPerCarrier
        = new Error(400, "You can only have one parcel locker selection per carrier");

    public static readonly Error InvalidParcelLockerChannel
        = new Error(400, "One or more selected parcel locker deliveries have invalid channel type");
}
