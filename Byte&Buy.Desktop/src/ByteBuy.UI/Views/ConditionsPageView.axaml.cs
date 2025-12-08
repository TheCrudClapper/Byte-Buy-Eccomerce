using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class ConditionsPageView : UserControl
{
    public ConditionsPageView()
    {
        InitializeComponent();
    }

    public ConditionsPageView(ConditionsPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}