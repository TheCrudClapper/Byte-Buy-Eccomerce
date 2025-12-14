using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ByteBuy.UI.ViewModels;
using System.IO;
using System.Threading.Tasks;

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