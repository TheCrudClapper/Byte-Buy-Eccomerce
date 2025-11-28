using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class CompanyInfoPageView : UserControl
{
    public CompanyInfoPageView()
    {
        InitializeComponent();
    }
    public CompanyInfoPageView(CompanyInfoPageView vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}