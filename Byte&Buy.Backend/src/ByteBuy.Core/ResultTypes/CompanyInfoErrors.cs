namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with company information
/// </summary>
public static class CompanyInfoErrors
{
    public static readonly Error DuplicateCompanyInfoObjects = new(
        ErrorType.Conflict, "CompanyInfo.DuplicateCompanyInfoObjects", "Company information is already defined, update it");
}
