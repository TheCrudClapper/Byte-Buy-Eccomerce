using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Companies.Errors;

/// <summary>
/// Class describes errors that might occur while working with company information
/// </summary>
public static class CompanyErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Company.NotFound", "Company is not found, you can't add offer");

    public static readonly Error DuplicateCompanyInfo = new(
        ErrorType.Conflict, "CompanyInfo", "Company information is already defined, update it");

    public static readonly Error CompanyNameInvalid = Error.Validation(
        "CompanyInfo.CompanyName",
        "Company name is required and must be at most 50 characters.");

    public static readonly Error TinInvalid = Error.Validation(
        "CompanyInfo.Tin",
        "TIN is required and must be at most 20 characters.");

    public static readonly Error EmailInvalid = Error.Validation(
        "CompanyInfo.Email",
        "Email is required and must be at most 50 characters.");

    public static readonly Error PhoneInvalid = Error.Validation(
        "CompanyInfo.Phone",
        "Phone is required and must be at most 16 characters.");

    public static readonly Error SloganInvalid = Error.Validation(
        "CompanyInfo.Slogan",
        "Slogan must be at most 30 characters.");
}
