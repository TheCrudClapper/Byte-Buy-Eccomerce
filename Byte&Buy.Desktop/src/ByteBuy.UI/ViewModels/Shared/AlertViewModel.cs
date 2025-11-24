using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class AlertViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = null!;
    
    [ObservableProperty]
    private string _message = null!;
    
    [ObservableProperty]
    private bool _isVisible;

    public async Task Show(AlertType alertType, string message)
    {
        Message = message;
        Title = alertType.ToString();
        IsVisible = true;

        await Task.Delay(2000);
        IsVisible = false;
    }

    public void Hide()
    {
        IsVisible = false;
    }
    
}

public enum  AlertType
{
    Info = 0,
    Warning = 1,
    Error = 2,
    Success = 3
};