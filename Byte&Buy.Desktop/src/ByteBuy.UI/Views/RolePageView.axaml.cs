using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class RolePageView : UserControl
{
    public RolePageView()
    {
        InitializeComponent();
    }
    public RolePageView(RolePageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}