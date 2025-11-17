using Avalonia.Controls;
using ByteBuy.Desktop.ViewModels;

namespace ByteBuy.Desktop.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }
    public LoginWindow(LoginWindowViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}