using System;
using System.Threading.Tasks;
using ByteBuy.Services.DTO;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class LoginWindowViewModel(IAuthService authService) : ViewModelBase
{

    [ObservableProperty] private string _email = string.Empty;

    [ObservableProperty] private string _password = string.Empty;

    [ObservableProperty] private string _error = string.Empty;

    public event Action? LoginSucceded;

    [RelayCommand]
    private async Task Login()
    {
        LoginRequest request = new(Email, Password);
        var result = await authService.Login(request);

        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }
        
        Email = string.Empty;
        Password = string.Empty;
        LoginSucceded?.Invoke();
    }
}
    
