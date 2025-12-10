namespace ByteBuy.Core.ResultTypes;

public static class CountryErrors
{
    public static readonly Error NotFound = new Error(
        404, "Country was not found");

    public static readonly Error AlreadyExists = new Error
       (400, "Country with this name or code already exists");

    public static readonly Error InUse= new Error
      (400, "Country is used, cannot be deleted");
}
