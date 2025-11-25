using ByteBuy.UI.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels;

public partial class DashboardPageViewModel : Base.PageViewModel
{
    public DashboardPageViewModel()
    {
        PageName = ApplicationPageNames.Dashboard;
    }
    
    [ObservableProperty]
    private string _test= "Just a test";
}