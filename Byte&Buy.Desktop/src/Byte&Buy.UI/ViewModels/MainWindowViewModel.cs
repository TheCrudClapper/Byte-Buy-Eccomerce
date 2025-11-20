using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDashboardPageActive))]
        [NotifyPropertyChangedFor(nameof(IsDashboardPageActive))]
        [NotifyPropertyChangedFor(nameof(IsSettingPageActive))]
        private ViewModelBase _currentPage;

        public Action? Logout { get; set; }
        
        private readonly DashboardPageViewModel _dashboardPage;
        private readonly EmployeesPageViewModel _employeesPage;
        private readonly SettingsPageViewModel _settingsPage;
        public bool IsDashboardPageActive => CurrentPage == _dashboardPage;
        public bool IsEmployeesPageActive => CurrentPage == _employeesPage;
        public bool IsSettingPageActive => CurrentPage == _settingsPage;
        
        public MainWindowViewModel(
            DashboardPageViewModel dashboardPage,
            EmployeesPageViewModel employeesPage,
            SettingsPageViewModel settingsPage)
        {
            _settingsPage = settingsPage;
            _dashboardPage = dashboardPage;
            _employeesPage = employeesPage;
            _currentPage = _dashboardPage;
        }

        [RelayCommand]
        private void GoToDashboard() => CurrentPage = _dashboardPage;
        
        [RelayCommand]
        private void GoToEmployees() => CurrentPage = _employeesPage;
        
        [RelayCommand]
        private void GoToSettings() => CurrentPage = _settingsPage;

        [RelayCommand]
        private void LogOut() =>  Logout?.Invoke();
    }
}
