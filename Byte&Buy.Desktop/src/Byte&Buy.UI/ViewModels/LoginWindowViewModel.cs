using System.Threading.Tasks;
using ByteBuy.Services.DTO;
using ByteBuy.Services.ServiceContracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class LoginWindowViewModel(IAuthService authService) : ViewModelBase
{

    [ObservableProperty] private string _email = "test";

    [ObservableProperty] private string _password = null!;

    [ObservableProperty] private string _error = null!;
    

    [RelayCommand]
    private async Task Login()
    {
        LoginRequest request = new(Email, Password);
        var result = await authService.Login(request);

        if (!result.Success)
        {
            Error = result.Error!.Detail;
        }
        //run main window here
    }
}
    
