namespace ByteBuy.Core.ResultTypes;

public static class CountryErrors
{
    public static readonly Error NotFound = new Error(
        404, "Country was not found");

    public static readonly Error AlreadyExists = new Error
       (400, "Condition with this name or code already exists");
}
