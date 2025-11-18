using Avalonia.Controls;
using LoginWindowViewModel = ByteBuy.UI.ViewModels.LoginWindowViewModel;

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