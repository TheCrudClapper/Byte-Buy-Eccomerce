namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with company information
/// </summary>
public static class CompanyInfoErrors
{
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
