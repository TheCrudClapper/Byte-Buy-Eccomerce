using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Shared;

public enum AlertType
{
    Info = 0,
    Warning = 1,
    Error = 2,
    Success = 3
};

public partial class AlertViewModel : ObservableValidator
{
    [ObservableProperty] private string _title = string.Empty;

    [ObservableProperty] private string _message = string.Empty;

    [ObservableProperty] private string _imagePath = string.Empty;

    [ObservableProperty] private string _backgroundColor = string.Empty;

    [ObservableProperty] private bool _isVisible;

    private CancellationTokenSource? _autoHideCts;
    public void Show(AlertType alertType, string message, int durationMs = 2000)
    {
        _autoHideCts?.Cancel();
        _autoHideCts = new CancellationTokenSource();

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

        IsVisible = true;
        _ = AutoHideAsync(durationMs, _autoHideCts.Token);
    }

    public void ShowErrorAlert(string message)
        => Show(AlertType.Error, message);

    public void ShowSuccessAlert(string message)
        => Show(AlertType.Success, message);

    [RelayCommand]
    private void Hide()
    {
        _autoHideCts?.Cancel();
        IsVisible = false;
    }

    private async Task AutoHideAsync(int durationMs, CancellationToken token)
    {
        try
        {
            await Task.Delay(durationMs, token);
            if (!token.IsCancellationRequested)
                IsVisible = false;
        }
        catch (TaskCanceledException)
        {
            //ignore
        }
    }

}