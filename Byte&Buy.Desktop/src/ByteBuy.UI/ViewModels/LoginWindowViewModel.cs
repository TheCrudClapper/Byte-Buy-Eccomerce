using ByteBuy.Services.DTO;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;


public partial class LoginWindowViewModel : WindowViewModel
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

    [ObservableProperty]
    private string _error = string.Empty;

    private readonly IAuthService _authService;
    public LoginWindowViewModel(IAuthService authService)
    {
        _authService = authService;
        WindowName = ApplicationWindowNames.Login;
    }

    public event Action? LoginSuccess;

    [RelayCommand]
    private async Task Login()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        LoginRequest request = new(Email, Password);
        var result = await _authService.Login(request);

        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }
        LoginSuccess?.Invoke();
    }
}