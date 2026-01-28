namespace ByteBuy.Core.ResultTypes;

public static class OrderErrors
{
    public static readonly Error QuantityInvalid = Error.Validation(
        "OrderLine.Quantity","Quantity must be greater than 0.");

    public static readonly Error RentalDaysInvalid = Error.Validation(
        "OrderLine.RentalDays", "Rental days msut be greater than 0, at least 1");
}
