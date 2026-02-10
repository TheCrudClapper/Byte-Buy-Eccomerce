using Avalonia.Controls;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels;


namespace ByteBuy.UI.Views;

public partial class MainWindow : Window
{
    private readonly WindowFactory _windowFactory = null!;

    //For designer
    public MainWindow()
    {
        InitializeComponent();
    }

    public MainWindow(MainWindowViewModel vm, WindowFactory factory)
    {
        InitializeComponent();
        _windowFactory = factory;
        DataContext = vm;
        vm.Logout += OnLogout;
    }

    private void OnLogout()
    {
        var loginWindow = _windowFactory.GetWindow(ApplicationWindowNames.Login);
        loginWindow.Show();
        Hide();
    }
}