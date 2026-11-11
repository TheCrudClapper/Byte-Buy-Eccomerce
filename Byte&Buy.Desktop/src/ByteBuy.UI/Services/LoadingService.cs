using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.Services;

public interface ILoadingService
{
    void Show(string? message = null);
    void Hide();
}

public partial class LoadingService : ObservableObject, ILoadingService
{
    [ObservableProperty]
    private bool _isLoading;
    [ObservableProperty]
    private string? _message;

    private int _pendingOperations = 0;
    public void Hide()
    {
        if (_pendingOperations > 0)
            _pendingOperations--;

        IsLoading = _pendingOperations > 0;
    }

    public void Show(string? message)
    {
        if(!string.IsNullOrWhiteSpace(message))
            Message = message;

        _pendingOperations++;
        IsLoading = true;
    }
}
