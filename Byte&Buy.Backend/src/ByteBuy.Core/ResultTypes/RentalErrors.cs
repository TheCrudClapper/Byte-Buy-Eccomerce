namespace ByteBuy.Core.ResultTypes;

public static class RentalErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Rental.NotFound", "Rental is not found");

    public static readonly Error QuantityInvalid = Error.Validation(
        "Rental.Quantity", "Quantity must be greater than 0");

    public static readonly Error RentalDaysInvalid = Error.Validation(
        "Rental.RentalDays", "RentalDays must be greater than 0");
}
