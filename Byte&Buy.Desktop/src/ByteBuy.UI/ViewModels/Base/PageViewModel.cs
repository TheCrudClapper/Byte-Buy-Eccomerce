using ByteBuy.UI.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Base;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationPageNames _pageName;
}