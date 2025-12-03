namespace ByteBuy.Core.ResultTypes;

public static class CountryErrors 
{
    public static readonly Error NotFound = new Error(
        404, "Country was not found");
}
