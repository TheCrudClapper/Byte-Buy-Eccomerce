using Avalonia.Controls;
using ByteBuy.Desktop.Views;
using LoginWindowViewModel = ByteBuy.UI.ViewModels.LoginWindowViewModel;

namespace ByteBuy.UI.Views;

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

        vm.LoginSucceded += OnLoginSucceded;
    }

    private void OnLoginSucceded()
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        
        this.Close();
    }
}