using Avalonia.Controls;
using Avalonia.Interactivity;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels;
using System.Linq;


namespace ByteBuy.UI.Views;

public partial class MainWindow : Window
{
    private readonly WindowFactory _windowFactory = null!;

    //For designer
    public MainWindow()
    {
        InitializeComponent();
    }

    private void SidebarButton_Click(object? sender, RoutedEventArgs e)
    {
        foreach (var btn in SidebarStackPanel.Children.OfType<Button>())
            btn.Classes.Remove("active");

        if (sender is Button clicked)
            clicked.Classes.Add("active");
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