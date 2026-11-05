using ByteBuy.Services.DTO;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;


public partial class LoginWindowViewModel : WindowViewModel
{
    #region MVVM Properties
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

    #endregion
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    private readonly IWindowService _windowService;
    public LoginWindowViewModel(IAuthService authService, IDialogService dialogService, IWindowService windowService)
    {
        _authService = authService;
        _dialogService = dialogService;
        _windowService = windowService;
    }

    [RelayCommand]
    private async Task OpenSettingsDialog()
    {
        await _dialogService.OpenDialogAsync(
            ApplicationDialogNames.Settings,
            hostIdentifier: "LoginDialogHost", init: async vm =>
            {
                if (vm is SettingsDialogViewModel settings)
                    await settings.Initialize();
            });
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        LoginRequest request = new(Email, Password);
        var result = await _authService.LoginAsync(request);

        if (!result.Success)
        {
            Error = result.Error!.Description;
            return;
        }

        _windowService.ShowWindow(ApplicationWindowNames.Main);
    }

    [RelayCommand]
    private void Shutdown() =>
        _windowService.ShutdownApplication();
}