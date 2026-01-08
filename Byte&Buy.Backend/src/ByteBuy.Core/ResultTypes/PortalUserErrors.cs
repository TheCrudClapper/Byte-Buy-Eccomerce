namespace ByteBuy.Core.ResultTypes;

public static class PortalUserErrors
{
    public static readonly Error DuplicateShippingAddressLabel = new(
        ErrorType.Conflict,
        "PortalUser.Address.DuplicateShippingAddressLabel",
        "Shipping address with this label already exists.");

    public static readonly Error CannotUnsetCurrentDefault = new(
        ErrorType.Conflict,
        "PortalUser.Address.CannotUnsetCurrentDefault",
        "Cannot unset the default address. Please set another address as default first.");

    public static readonly Error CannotDeleteCurrentDefault = new(
        ErrorType.Conflict,
        "PortalUser.Address.CannotDeleteCurrentDefault",
        "Cannot delete the default address. Please set another address as default first.");

}
