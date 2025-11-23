using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;


public partial class LoginWindowViewModel(IAuthService authService) : ViewModelBase
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    private string _password = string.Empty;

    [ObservableProperty] private string _error = string.Empty;

    public event Action? LoginSucceded;

    [RelayCommand]
    private async Task Login()
    {
        // ValidateAllProperties();
        // if (HasErrors)
        //     return;
        //
        // LoginRequest request = new(Email, Password);
        // var result = await authService.Login(request);
        //
        // if (!result.Success)
        // {
        //     Error = result.Error!.Description;
        //     return;
        // }
        //
        // // //Clean fields
        // Email = string.Empty;
        // Password = string.Empty;
        // Error = string.Empty;
        LoginSucceded?.Invoke();
    }
}