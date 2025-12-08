using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class AdministrationPageView : UserControl
{
    public AdministrationPageView()
    {
        InitializeComponent();
    }
    public AdministrationPageView(AdministrationPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}