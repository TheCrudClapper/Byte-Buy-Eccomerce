namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with offer aggregate
/// </summary>
public static class OfferErrors
{

    public static readonly Error SoldOut = new(
        ErrorType.Validation, "Offer.SoldOut", "Offer is sold out and is not available for purchase.");

    public static readonly Error QuantityDecreaseInvalid = new(
        ErrorType.Validation, "Offer.Quantity", "User requested more quantity than is avaliable");

    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Offer.NotFound", "Offer is not found.");

    public static readonly Error DeliveryRequired = new(
        ErrorType.Validation, "Offer.DeliveryRequired", "At least one other delivery is required.");

    public static readonly Error MultipleParcelLockersPerCarrier = new(
        ErrorType.Validation, "Offer.MultipleParcelLockersPerCarrier",
        "You can only have one parcel locker selected per carrier.");

    public static readonly Error InvalidAdditionalQuantity = new(
       ErrorType.Validation, "Offer.AdditionalQuantity", "Additional quantity can either be 0 or positive");

    public static readonly Error InvalidAdditionalRentalDays = Error.Validation(
        "RentOffer.AdditionalRentalDays", "Additional rental days must be either 0 or positive");

    public static readonly Error InvalidRentalDaysSum = Error.Validation(
        "RentOffer.AdditionalRentalDays", "Current max rentals days and additional sum must be lower than 360.");

    public static readonly Error InvalidParcelLockerChannel = new(
        ErrorType.Validation, "Offer.InvalidParcelLockerChannel",
        "One or more selected parcel locker deliveries have invalid channel type.");

    public static readonly Error QuantityInvalid = Error.Validation(
        "Offer.Quantity",
        "Quantity must be greater than 0.");

    public static readonly Error ItemNotFound = Error.Validation(
        "Offer.Item", "Item is not found");

    public static readonly Error MaxRentalDaysInvalid = Error.Validation(
        "RentOffer.MaxRentalDays", "Max rental days must be at least one and 360 at max");
}
