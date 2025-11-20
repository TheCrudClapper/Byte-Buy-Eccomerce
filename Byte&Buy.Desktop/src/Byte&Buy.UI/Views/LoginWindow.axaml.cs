using System;
using Avalonia.Controls;
using ByteBuy.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using LoginWindowViewModel = ByteBuy.UI.ViewModels.LoginWindowViewModel;

namespace ByteBuy.UI.Views;

public partial class LoginWindow : Window
{
    private readonly IServiceProvider _serviceProvider = null!;
    
    public LoginWindow()
    {
        InitializeComponent();
    }
    
    public LoginWindow(LoginWindowViewModel vm, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        
        DataContext = vm;
        _serviceProvider = serviceProvider;
        vm.LoginSucceded += OnLoginSucceded;
    }

    private void OnLoginSucceded()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show(); 
        Hide();
    }
}