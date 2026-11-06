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

public partial class AlertViewModel : ObservableObject
{
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private string _message = string.Empty;
    [ObservableProperty] private bool _isVisible;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsInfo), nameof(IsWarning), nameof(IsError), nameof(IsSuccess))]
    private AlertType _type;

    public bool IsInfo => Type == AlertType.Info;
    public bool IsWarning => Type == AlertType.Warning;
    public bool IsError => Type == AlertType.Error;
    public bool IsSuccess => Type == AlertType.Success;

    private CancellationTokenSource? _autoHideCts;

    public void Show(AlertType alertType, string message, int durationMs = 2000)
    {
        _autoHideCts?.Cancel();
        _autoHideCts = new CancellationTokenSource();

        Type = alertType;
        Title = alertType.ToString();
        Message = message;
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