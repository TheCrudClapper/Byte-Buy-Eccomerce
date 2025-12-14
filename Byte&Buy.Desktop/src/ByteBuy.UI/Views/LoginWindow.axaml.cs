using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels;
using Microsoft.Extensions.Hosting.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace ByteBuy.UI.Views;

public partial class LoginWindow : Window
{
    private readonly WindowFactory _windowFactory = null!;
    private readonly IClassicDesktopStyleApplicationLifetime _applicationLifetime = null!;

    public LoginWindow()
    {
        InitializeComponent();
    }

    public LoginWindow(LoginWindowViewModel vm, WindowFactory windowFactory, IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        InitializeComponent();
        DataContext = vm;
        _windowFactory = windowFactory;
        _applicationLifetime = applicationLifetime;
        vm.LoginSuccess += OnLoginSuccess;
    }

    private void OnLoginSuccess()
    {
        var mainWindow = _windowFactory.GetWindow(ApplicationWindowNames.Main);
        mainWindow.Show();
        _applicationLifetime.MainWindow = mainWindow;
        Close();
    }
}