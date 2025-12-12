using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
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

    private void OnDragEnter(object? sender, DragEventArgs e)
    {

    }
}