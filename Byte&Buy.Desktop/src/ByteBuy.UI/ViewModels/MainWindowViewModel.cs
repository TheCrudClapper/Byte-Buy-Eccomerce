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
        [NotifyPropertyChangedFor(nameof(IsProfilePageActive))]
        private ViewModelBase _currentPage;

        public Action? Logout { get; set; }
        
        private readonly DashboardPageViewModel _dashboardPage;
        private readonly EmployeesPageViewModel _employeesPage;
        private readonly SettingsPageViewModel _settingsPage;
        private readonly ProfilePageViewModel _profilePage;
        private readonly EmployeePageViewModel _employeePage;
        
        public bool IsProfilePageActive => CurrentPage == _profilePage;
        public bool IsDashboardPageActive => CurrentPage == _dashboardPage;
        public bool IsEmployeesPageActive => CurrentPage == _employeesPage;
        public bool IsSettingPageActive => CurrentPage == _settingsPage;
        public bool IsEmployeePageActive => CurrentPage == _employeePage;
        
        public MainWindowViewModel(
            DashboardPageViewModel dashboardPage,
            EmployeesPageViewModel employeesPage,
            SettingsPageViewModel settingsPage,
            ProfilePageViewModel profilePage,
            EmployeePageViewModel employeePage)
        {
            _settingsPage = settingsPage;
            _dashboardPage = dashboardPage;
            _employeesPage = employeesPage;
            _profilePage = profilePage;
            _employeePage = employeePage;
            _currentPage = _dashboardPage;
        }

        [RelayCommand]
        private void GoToDashboard() => CurrentPage = _dashboardPage;
        
        [RelayCommand]
        private void GoToEmployees() => CurrentPage = _employeesPage;
        
        [RelayCommand]
        private void GoToSettings() => CurrentPage = _settingsPage;
        
        [RelayCommand]
        private void GoToProfile() => CurrentPage = _profilePage;

        [RelayCommand]
        private void GoToEmployee() => CurrentPage = _employeePage;
        
        [RelayCommand]
        private void LogOut() =>  Logout?.Invoke();
    }
}
