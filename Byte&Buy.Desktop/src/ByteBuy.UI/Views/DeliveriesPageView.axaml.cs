using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class DeliveriesPageView : UserControl
{
    public DeliveriesPageView()
    {
        InitializeComponent();
    }

    public DeliveriesPageView(DeliveriesPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}