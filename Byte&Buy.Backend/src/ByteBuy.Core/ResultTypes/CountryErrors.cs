namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with country aggregate
/// </summary>
public static class CountryErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Country.NotFound", "Country is not found");

    public static readonly Error AlreadyExists = new(
        ErrorType.Conflict, "Country.AlreadyExitst", "Country with this data already exists");

    public static readonly Error HasActiveAddresses = new(
        ErrorType.Conflict, "Country.HasActiveAddresses", "Country is used, cannot be deleted");
}
