using Avalonia.Threading;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ByteBuy.UI.Services;

public interface INotificationService
{
    ObservableCollection<NotificationViewModel> Notifications { get; }
    void Show(string message, string title, NotificationType type, int durationMs = 2000);
    void Dismiss(NotificationViewModel notificationViewModel);
    void ShowSuccess(string message, int durationMs = 2000);
    void ShowError(string message, int durationMs = 2000);
    void ShowWarning(string message, int durationMs = 2000);
    void ShowInfo(string message, int durationMs = 2000);
}

public class NotificationService : ObservableObject, INotificationService
{
    public ObservableCollection<NotificationViewModel> Notifications { get => _notifications; }
    private ObservableCollection<NotificationViewModel> _notifications = [];
    public void Show(string message, string title, NotificationType type, int durationMs = 2000)
    {
        NotificationViewModel? notification = null;

        notification = new NotificationViewModel(
           title,
           message,
           type,
           new RelayCommand(() => Dismiss(notification!)));
        _notifications.Add(notification);

        if (durationMs > 0)
        {
            _ = Task.Delay(durationMs).ContinueWith(_ => Dispatcher.UIThread.Post(() => Notifications.Remove(notification)));
        }
    }

    public void Dismiss(NotificationViewModel notification)
    {
        _notifications.Remove(notification);
    }

    public void ShowSuccess(string message, int durationMs = 2000)
        => Show(message, "Success", NotificationType.Success, durationMs);

    public void ShowWarning(string message, int durationMs = 2000)
        => Show(message, "Warning", NotificationType.Warning, durationMs);

    public void ShowInfo(string message, int durationMs = 2000)
        => Show(message, "Info", NotificationType.Info, durationMs);

    public void ShowError(string message, int durationMs = 2000)
        => Show(message, "Error", NotificationType.Error, durationMs);
}
