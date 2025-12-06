namespace ByteBuy.Core.ResultTypes;

public static class AddressErrors 
{
    public static readonly Error DuplicateLabel = new Error(
        400, "Address with this label alerady exists");

    public static readonly Error CannotUnsetCurrentDefault = new Error(
        400, "Cannot unset the default address. Please set another address as default first");

    public static readonly Error CannotDeleteCurrentDefault = new Error(
        400, "Cannot delete default addres. Please set another address as default first");
}

