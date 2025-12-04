using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class AlertViewModel : ViewModelBase
{
    [ObservableProperty] private string _title = null!;

    [ObservableProperty] private string _message = null!;

    [ObservableProperty] private string _imagePath = null!;

    [ObservableProperty] private string _backgroundColor = null!;

    [ObservableProperty] private bool _isVisible;

    public Task Show(AlertType alertType, string message)
    {
        Message = message;
        Title = alertType.ToString();

        switch (alertType)
        {
            case AlertType.Info:
                BackgroundColor = "#5394fc";
                ImagePath = "avares://ByteBuy.UI/Assets/Images/regular/circle-info-solid-full.svg";
                break;

            case AlertType.Error:
                BackgroundColor = "#EF4444";
                ImagePath = "avares://ByteBuy.UI/Assets/Images/regular/triangle-exclamation-solid-full.svg";
                break;

            case AlertType.Success:
                BackgroundColor = "#22C55E";
                ImagePath = "avares://ByteBuy.UI/Assets/Images/regular/check-solid-full.svg";
                break;

            case AlertType.Warning:
                BackgroundColor = "#f7e963";
                ImagePath = "avares://ByteBuy.UI/Assets/Images/regular/exclamation-solid-full.svg";
                break;
        }

        _ = AutoHide();
        return Task.CompletedTask;
    }

    public async Task ShowErrorAlert(string message)
        => await Show(AlertType.Error, message);

    public async Task ShowSuccessAlert(string message)
        => await Show(AlertType.Success, message);

    [RelayCommand]
    private void Hide()
    {
        IsVisible = false;
    }

    private async Task AutoHide()
    {
        IsVisible = true;
        await Task.Delay(2000);
        IsVisible = false;
    }
}

public enum AlertType
{
    Info = 0,
    Warning = 1,
    Error = 2,
    Success = 3
};