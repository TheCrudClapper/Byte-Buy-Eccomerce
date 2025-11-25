using System;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels
{
    public partial class MainWindowViewModel : WindowViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDashboardPageActive))]
        [NotifyPropertyChangedFor(nameof(IsSettingPageActive))]
        [NotifyPropertyChangedFor(nameof(IsEmployeesPageActive))]
        [NotifyPropertyChangedFor(nameof(IsProfilePageActive))]
        [NotifyPropertyChangedFor(nameof(IsRolesPageActive))]
        private PageViewModel _currentPage;
        
        //Factory For Pages
        private readonly PageFactory _pageFactory;
       
        public Action? Logout { get; set; }

        public bool IsProfilePageActive => CurrentPage.PageName == ApplicationPageNames.Profile;
        public bool IsDashboardPageActive => CurrentPage.PageName == ApplicationPageNames.Dashboard;
        public bool IsEmployeesPageActive => CurrentPage.PageName == ApplicationPageNames.Employees;
        public bool IsSettingPageActive => CurrentPage.PageName == ApplicationPageNames.Settings;
        public bool IsRolesPageActive => CurrentPage.PageName == ApplicationPageNames.Roles;

        public MainWindowViewModel(PageFactory pageFactory)
        {
            _pageFactory = pageFactory;
            CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Dashboard);
        }
        
        [RelayCommand]
        private void GoToDashboard() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Dashboard);
        
        [RelayCommand]
        private void GoToEmployees() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Employees);
        
        [RelayCommand]
        private void GoToSettings() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Settings);
        
        [RelayCommand]
        private void GoToProfile() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Profile);
        
        [RelayCommand]
        private void GoToRoles() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Roles);
        
        [RelayCommand]
        private void LogOut() =>  Logout?.Invoke();
    }
}
