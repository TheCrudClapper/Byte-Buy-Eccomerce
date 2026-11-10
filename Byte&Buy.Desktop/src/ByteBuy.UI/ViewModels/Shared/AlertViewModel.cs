using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ByteBuy.UI.ViewModels.Shared;

public enum NotificationType
{
    Info = 0,
    Warning = 1,
    Error = 2,
    Success = 3
};

public partial class NotificationViewModel : ObservableObject
{
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private string _message = string.Empty;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsInfo), nameof(IsWarning), nameof(IsError), nameof(IsSuccess))]
    private NotificationType _type;

    //Command that disposed the alert
    public ICommand HideCommand { get; }
    public bool IsInfo => Type == NotificationType.Info;
    public bool IsWarning => Type == NotificationType.Warning;
    public bool IsError => Type == NotificationType.Error;
    public bool IsSuccess => Type == NotificationType.Success;

    public NotificationViewModel(string title, string message, NotificationType type, ICommand hideCommand)
    {
        Title = title;
        Message = message;
        HideCommand = hideCommand;
        Type = type;
    }
}