using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Orders.Errors;

public static class OrderErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Order", "Order is not found");

    public static readonly Error NotSuitableForReturn = new(
        ErrorType.Validation, "Order.Return", "Cannot return order that doesnt contain any offers for sale");

    public static readonly Error DeactivationImpossible = new(
        ErrorType.Validation, "Order.Delete", "Cannot delete and order that is not delivered yet !");

    public static readonly Error OrderStillHasReturnPeriod = new(
        ErrorType.Validation, "Order.Delete", "Order still can be returned, thus it can't be deleted!");

    public static readonly Error FailedToCreateOrder = new(
        ErrorType.Unexpected, "Order", "Failed to create order, try again later");

    public static readonly Error InvalidReturnState = Error.Validation(
        "Order.OrderStatus", "Only delivered orders can be returned");

    public static readonly Error InvalidDeliveredState = Error.Validation(
      "Order.OrderStatus", "Only delivered orders can be returned");

    public static readonly Error InvalidDelivery = new(
        ErrorType.Validation, "Order.Delivery", "Some deliveries sent by user are not valid");

    public static readonly Error ReturnPeriodExpired = new(
        ErrorType.Validation, "Order.OrderStatus", "Order cannot be returned after 14 days counting from delivery.");

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
     "Order.OrderStatus", "Order cannot be shipped because it has not been paid for.");
}
