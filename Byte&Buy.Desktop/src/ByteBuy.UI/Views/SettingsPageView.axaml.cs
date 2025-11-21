using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class SettingsPageView : UserControl
{
    public SettingsPageView()
    {
        InitializeComponent();
    }
    
    public SettingsPageView(SettingsPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}