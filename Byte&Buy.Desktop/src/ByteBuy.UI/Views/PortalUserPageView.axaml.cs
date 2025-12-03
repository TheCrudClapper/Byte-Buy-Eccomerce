using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class PortalUserPageView : UserControl
{
    public PortalUserPageView()
    {
        InitializeComponent();
    }
    public PortalUserPageView(PortalUserPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}