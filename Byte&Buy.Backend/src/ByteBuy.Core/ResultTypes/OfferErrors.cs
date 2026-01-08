namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with offer aggregate
/// </summary>
public static class OfferErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Offer.NotFound", "Offer is not found");

    public static readonly Error DeliveryRequired = new(
        ErrorType.Validation, "Offer.DeliveryRequired", "At least one other delivery is required");

    public static readonly Error MultipleParcelLockersPerCarrier = new(
        ErrorType.Validation, "Offer.MultipleParcelLockersPerCarrier",
        "You can only have one parcel locker selected per carrier");

    public static readonly Error InvalidParcelLockerChannel = new(
        ErrorType.Validation, "Offer.InvalidParcelLockerChannel",
        "One or more selected parcel locker deliveries have invalid channel type");
}
