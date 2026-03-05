using ByteBuy.UI.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.UI.DataAdnotations;

public class StockQuantityValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not ItemPageViewModel vm)
            return ValidationResult.Success;

        int stock = value as int? ?? 0;

        if (!vm.IsEditMode && stock < 1)
            return new ValidationResult("Stock quantity must be at least 1 when adding new item");

        if (vm.IsEditMode && stock < 0)
            return new ValidationResult("Stock quantity cannot be negative");

        return ValidationResult.Success;

    }
}
