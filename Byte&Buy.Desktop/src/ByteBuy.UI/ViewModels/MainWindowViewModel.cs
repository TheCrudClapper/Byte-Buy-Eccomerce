using ByteBuy.UI.Data;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.ManyViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class MainWindowViewModel : WindowViewModel
{
    #region MVVM Fields
    [ObservableProperty]
    private bool _isDashboardPageActive;

    [ObservableProperty]
    private bool _isEmployeesPageActive;

    [ObservableProperty]
    private bool _isRolesPageActive;

    [ObservableProperty]
    private bool _isPermissionsPageActive;

    [ObservableProperty]
    private bool _isAdministrationPageActive;

    [ObservableProperty]
    private bool _isPortalUsersPageActive;

    [ObservableProperty]
    private bool _isCompanyPageActive;

    [ObservableProperty]
    private bool _isOrdersPageActive;

    [ObservableProperty]
    private bool _isRentalsPageActive;

    [ObservableProperty]
    private bool _isProfilePageActive;
    #endregion

    public PageViewModel? CurrentPage => _navigation.CurrentPage;

    private readonly INavigationService _navigation;

    private void SetActive(ApplicationPageNames page)
    {
        IsDashboardPageActive = page == ApplicationPageNames.Dashboard;
        IsEmployeesPageActive = page  == ApplicationPageNames.Employees;
        IsRolesPageActive = page == ApplicationPageNames.Roles;
        IsPermissionsPageActive = page == ApplicationPageNames.Permissions;
        IsAdministrationPageActive = page == ApplicationPageNames.Administration;
        IsPortalUsersPageActive = page == ApplicationPageNames.PortalUsers;
        IsRentalsPageActive = page == ApplicationPageNames.Rentals;
        IsCompanyPageActive = page == ApplicationPageNames.CompanyInfo;
        IsOrdersPageActive = page == ApplicationPageNames.Orders;
        IsProfilePageActive = page == ApplicationPageNames.Profile;
    }

    public Action? Logout { get; set; }

    public MainWindowViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        _navigation.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(INavigationService.CurrentPage))
            {
                OnPropertyChanged(nameof(CurrentPage));
                SetActive(_navigation.CurrentPage!.PageName);
            }
        };
        _ = GoToDashboard();
    }

    [RelayCommand]
    private async Task GoToDashboard() => await _navigation.NavigateToAsync(ApplicationPageNames.Dashboard, async vm =>
    {
        if (vm is DashboardPageViewModel dashVm)
            await dashVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToCompanyDetails() => await _navigation.NavigateToAsync(ApplicationPageNames.CompanyInfo, async vm =>
    {
        if (vm is CompanyInfoPageViewModel empVm)
            await empVm.LoadData();
    });

    [RelayCommand]
    private async Task GoToPortalUsers() => await _navigation.NavigateToAsync(ApplicationPageNames.PortalUsers, async vm =>
    {
        if (vm is PortalUsersPageViewModel empVm)
            await empVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToEmployees() => await _navigation.NavigateToAsync(ApplicationPageNames.Employees, async vm =>
    {
        if (vm is EmployeesPageViewModel empVm)
            await empVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToProfile() => await _navigation.NavigateToAsync(ApplicationPageNames.Profile, async vm =>
    {
        if (vm is ProfilePageViewModel profVm)
            await profVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToRoles() => await _navigation.NavigateToAsync(ApplicationPageNames.Roles, async vm =>
    {
        if (vm is RolesPageViewModel rolesVm)
            await rolesVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToPermissions() => await _navigation.NavigateToAsync(ApplicationPageNames.Permissions, async vm =>
    {
        if (vm is PermissionsPageViewModel permVm)
            await permVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToOrders() => await _navigation.NavigateToAsync(ApplicationPageNames.Orders, async vm =>
    {
        if (vm is OrdersPageViewModel ordersVm)
            await ordersVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToRentals() => await _navigation.NavigateToAsync(ApplicationPageNames.Rentals, async vm =>
    {
        if (vm is RentalsPageViewModel rentalsVm)
            await rentalsVm.LoadDataAsync();
    });


    [RelayCommand]
    private async Task GoToAdministration() => await _navigation.NavigateToAsync(ApplicationPageNames.Administration);

    [RelayCommand]
    private void LogOut() => Logout?.Invoke();
}
