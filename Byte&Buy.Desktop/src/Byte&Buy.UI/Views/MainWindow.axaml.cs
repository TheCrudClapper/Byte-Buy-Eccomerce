using System;
using Avalonia.Controls;
using ByteBuy.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.UI.Views
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider = null!;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        public MainWindow(MainWindowViewModel vm, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            DataContext = vm;
            vm.Logout += OnLogout;
        }

        private void OnLogout()
        {
            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            Hide();
        }
    }
}