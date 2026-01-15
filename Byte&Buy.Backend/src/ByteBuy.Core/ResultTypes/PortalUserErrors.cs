namespace ByteBuy.Core.ResultTypes;

public static class PortalUserErrors
{
    public static readonly Error DuplicateShippingAddressLabel = new(
        ErrorType.Conflict,
        "PortalUser.ShippingAddress.DuplicateShippingAddressLabel",
        "Shipping address with this label already exists.");

    public static readonly Error CannotUnsetCurrentDefault = new(
        ErrorType.Conflict,
        "PortalUser.ShippingAddress.CannotUnsetCurrentDefault",
        "Cannot unset the default address. Please set another address as default first.");

    public static readonly Error CannotDeleteCurrentDefault = new(
        ErrorType.Conflict,
        "PortalUser.ShippingAddress.CannotDeleteCurrentDefault",
        "Cannot delete the default address. Please set another address as default first.");

    public static readonly Error ShippingAddressLabelInvalid =
        Error.Validation("PortalUser.ShippingAddress", "Label is required and must be at most 50 characters.");

    public static readonly Error HomeAddressNotSet =
        Error.Validation("PortalUser.HomeAddress", "You need to set home address in order to publish offers.");

    public static readonly Error ShippingAddressNotFound =
        Error.Validation("PortalUser.ShippingAddress", "Given shipping address is not found");
}
