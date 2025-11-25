using ByteBuy.UI.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Base;

public partial class WindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationWindowNames _windowName;
}