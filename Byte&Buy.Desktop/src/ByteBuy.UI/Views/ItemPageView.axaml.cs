using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class ItemPageView : UserControl
{
    public ItemPageView()
    {
        InitializeComponent();
    }
    public ItemPageView(ItemPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}