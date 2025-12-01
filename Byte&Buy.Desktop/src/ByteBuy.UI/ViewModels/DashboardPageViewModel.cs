using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels;

public partial class DashboardPageViewModel : Base.PageViewModel
{
    public DashboardPageViewModel(AlertViewModel alert) : base(alert)
    {
        PageName = ApplicationPageNames.Dashboard;
    }

    [ObservableProperty]
    private string _test = "Just a test";
}