using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

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