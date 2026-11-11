using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ByteBuy.UI.Attributes;

public partial class UrlValidatorAttribute : ValidationAttribute
{
    //skips validation for null or empty string values
    public bool OmitNullOrEmpty { get; set; }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string url)
            return new ValidationResult("Value is not a string");

        if (OmitNullOrEmpty)
        {
            if (string.IsNullOrWhiteSpace(url))
                return ValidationResult.Success;
        }

        Match result = UrlRegex().Match(url);
        if (!result.Success)
            return new ValidationResult("Given value is not a valid url");

        return ValidationResult.Success;
    }

    [GeneratedRegex(@"https?://(www\.)?([-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}|localhost)(:[0-9]{1,5})?\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)")]
    private static partial Regex UrlRegex();
}
