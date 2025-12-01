using Avalonia.Controls;

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