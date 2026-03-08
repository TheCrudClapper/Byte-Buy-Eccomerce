using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class PasswordChangeViewModel(IEmployeeService employeeService)
    : ObservableValidator
{
    [ObservableProperty]
    [Required(ErrorMessage = "Current Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    private string _currentPassword = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Confirm Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "New Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    private string _newPassword = string.Empty;

    [ObservableProperty]
    private string _error = string.Empty;

    partial void OnNewPasswordChanged(string value)
        => ValidateProperty(ConfirmPassword, nameof(ConfirmPassword));

    partial void OnConfirmPasswordChanged(string value)
        => ValidateProperty(ConfirmPassword, nameof(ConfirmPassword));

    [RelayCommand]
    private async Task ChangePassword()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        var request = new PasswordChangeRequest(NewPassword, CurrentPassword, ConfirmPassword);
        var response = await employeeService.ChangePasswordAsync(request);
        if (!response.Success)
            Error = response?.Error?.Description ?? "Error occured";
    }

    [RelayCommand]
    private void ClearFields()
    {
        CurrentPassword = string.Empty;
        NewPassword = string.Empty;
        ConfirmPassword = string.Empty;
    }
}
