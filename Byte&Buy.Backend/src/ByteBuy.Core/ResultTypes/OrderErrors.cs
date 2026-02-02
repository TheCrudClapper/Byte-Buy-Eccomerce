namespace ByteBuy.Core.ResultTypes;

public static class OrderErrors
{
    public static readonly Error InvalidDelivery = new(
        ErrorType.Validation, "Order.Delivery", "Some deliveries sent by user are not valid");

    public static readonly Error QuantityInvalid = Error.Validation(
        "OrderLine.Quantity", "Quantity must be greater than 0.");

    public static readonly Error RentalDaysInvalid = Error.Validation(
        "OrderLine.RentalDays", "Rental days msut be greater than 0, at least 1");

    public static readonly Error NoCartOffersFound = new(
        ErrorType.NotFound, "Order.CartOffers", "No cart offers found, abandoning order creation.");

    public static readonly Error InvalidPaymentMethod = Error.Validation(
        "Order.PaymentMethod", "Given payment method doesn't exist");

    public static readonly Error MisingDeliveryPerSeller = Error.Validation(
        "Order.Deliveries", "Some sellers dont have any selected deliveries");

    public static readonly Error InvalidSeller = Error.Validation(
        "Order.Sellers", "Some sellers are not present in request");

    public static readonly Error EmptyLines = Error.Validation(
      "Order.Lines", "Order cannot be created without items");

    public static readonly Error EmptySellerDetails = Error.Validation(
      "Order.Seller", "Order cannot be created without seller information");

    public static readonly Error ShippedStatusViolation = Error.Validation(
     "Order.Status", "Order cannot be shipped because it has not been paid for.");
}
