using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Base;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationPageNames _pageName;
    
    public AlertViewModel Alert { get; }

    public PageViewModel(AlertViewModel alert)
    {
        Alert = alert;
    }
}