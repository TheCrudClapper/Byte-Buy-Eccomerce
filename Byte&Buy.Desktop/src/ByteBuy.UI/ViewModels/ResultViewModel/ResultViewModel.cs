using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Result;

public partial class ResultViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isSuccess;
    [ObservableProperty]
    private string _message = null!;
    public string StatusBrush => IsSuccess ? "SuccessBrush" : "ErrorBrush";
    private ResultViewModel(bool isSuccess, string message)
    {
        Message = message;
        IsSuccess = isSuccess;
    }

    public static ResultViewModel CreateError(string message)
        => new(false, message);

    public static ResultViewModel CreateSuccess(string message)
        => new(true, message);
}
