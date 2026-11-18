using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class CrashDialogViewModel : DialogViewModel
{
    [ObservableProperty] public string _title = null!;
    [ObservableProperty] public string? _stackTrace;
    [ObservableProperty] public string? _message;

    public CrashDialogViewModel() : base("Test")
    {

    }
}
