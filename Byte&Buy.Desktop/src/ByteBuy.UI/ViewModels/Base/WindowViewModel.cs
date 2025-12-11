using ByteBuy.UI.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Base;

public partial class WindowViewModel : ObservableValidator
{
    [ObservableProperty]
    private ApplicationWindowNames _windowName;
}