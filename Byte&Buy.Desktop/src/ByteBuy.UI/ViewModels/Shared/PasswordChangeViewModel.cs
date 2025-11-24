using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.HttpClients.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class PasswordChangeViewModel(IEmployeeService employeeService)
    : ViewModelBase
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

        var request = new PasswordChangeRequest(NewPassword!, CurrentPassword!, ConfirmPassword!);
        var response = await employeeService.ChangePassword(request);
        if (!response.Success)
           Error =  response.Error!.Description;
    }

    [RelayCommand]
    private void ClearFields()
    {
        CurrentPassword = string.Empty;
        NewPassword = string.Empty;
        ConfirmPassword = string.Empty;
    }
}
