using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class PortalUsersPageView : UserControl
{
    public PortalUsersPageView()
    {
        InitializeComponent();
    }
    public PortalUsersPageView(PortalUsersPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}