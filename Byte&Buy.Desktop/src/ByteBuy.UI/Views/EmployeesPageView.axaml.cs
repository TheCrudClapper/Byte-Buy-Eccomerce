using Avalonia.Controls;
using Avalonia.VisualTree;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class EmployeesPageView : UserControl
{
    public EmployeesPageView()
    {
        InitializeComponent();
    }
    public EmployeesPageView(EmployeesPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;   
    }
}