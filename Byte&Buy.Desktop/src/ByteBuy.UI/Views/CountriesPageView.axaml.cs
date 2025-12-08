using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class CountriesPageView : UserControl
{
    public CountriesPageView()
    {
        InitializeComponent();
    }
    public CountriesPageView(CountriesPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}