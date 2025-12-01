using Avalonia.Controls;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class LoginWindow : Window
{
    private readonly WindowFactory _windowFactory = null!;

    public LoginWindow()
    {
        InitializeComponent();
    }

    public LoginWindow(LoginWindowViewModel vm, WindowFactory windowFactory)
    {
        InitializeComponent();
        DataContext = vm;
        _windowFactory = windowFactory;
        vm.LoginSuccess += OnLoginSuccess;
    }

    private void OnLoginSuccess()
    {
        var mainWindow = _windowFactory.GetWindow(ApplicationWindowNames.Main);
        mainWindow.Show();
        Close();
    }
}