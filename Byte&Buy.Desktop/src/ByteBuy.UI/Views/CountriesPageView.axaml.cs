using Avalonia.Controls;
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