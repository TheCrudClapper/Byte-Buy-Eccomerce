using CommunityToolkit.Mvvm.ComponentModel;
namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class DialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _dialogTitle = string.Empty;

    protected DialogViewModel(string dialogTitle)
    {
        DialogTitle = dialogTitle;
    }
}
