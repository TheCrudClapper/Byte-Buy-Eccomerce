using Avalonia.Controls;
using ByteBuy.UI.ViewModels;
using DashboardPageViewModel = ByteBuy.UI.ViewModels.DashboardPageViewModel;

namespace ByteBuy.UI.Views;

public partial class DashboardPageView : UserControl
{
    public DashboardPageView()
    {
        InitializeComponent();
    }
    public DashboardPageView(DashboardPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}