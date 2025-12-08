using System.ComponentModel.DataAnnotations;

namespace ByteBuy.UI.DataAdnotations;

public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _conditionPropName;
    public RequiredIfAttribute(string conditionPropName)
        => _conditionPropName = conditionPropName;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_conditionPropName);
        if (property == null)
            return ValidationResult.Success;

        var conditionValue = property.GetValue(validationContext.ObjectInstance);
        if (conditionValue is bool b && b && string.IsNullOrWhiteSpace(value?.ToString()))
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} is required");

        return ValidationResult.Success;
    }
}
