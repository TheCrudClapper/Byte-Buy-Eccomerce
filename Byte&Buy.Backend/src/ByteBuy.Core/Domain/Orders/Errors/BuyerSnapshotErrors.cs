using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Orders.Errors;

public static class BuyerSnapshotErrors
{
    public static readonly Error HomeAddressNotSet = new(
      ErrorType.NotFound, "Buyer.HomeAddress", "You haven't set home address, its mandatory for order creation.");

    public static readonly Error PhoneNumberNotSet = new(
      ErrorType.NotFound, "Buyer.PhoneNumber", "You haven't set phone number, its mandatory for order creation");
}
