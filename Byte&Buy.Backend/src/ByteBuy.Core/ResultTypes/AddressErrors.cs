namespace ByteBuy.Core.ResultTypes;

public static class AddressErrors 
{
    public static readonly Error DuplicateLabel = new Error(
        400, "Address with this label alerady exists");
}
