using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels;

public partial class DashboardPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _test= "Just a test";
}