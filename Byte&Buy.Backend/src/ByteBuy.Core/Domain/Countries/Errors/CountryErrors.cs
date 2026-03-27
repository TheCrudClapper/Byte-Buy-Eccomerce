using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Countries.Errors;

/// <summary>
/// Class describes errors that might occur while working with country aggregate
/// </summary>
public static class CountryErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Country.NotFound", "Country is not found.");

    public static readonly Error AlreadyExists = new(
        ErrorType.Conflict, "Country.AlreadyExitst", "Country with this data already exists.");

    public static readonly Error HasActiveAddresses = new(
        ErrorType.Conflict, "Country.HasActiveAddresses", "Country is used, cannot be deleted.");

    public static readonly Error NameInvalid = Error.Validation(
        "Country.Name", "Name is required and must be at most 50 characters.");

    public static readonly Error CodeInvalid = Error.Validation(
        "Country.Code", "Code is required and must be at most 3 characters.");
}
