using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.ManyViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class MainWindowViewModel : WindowViewModel, IDisposable
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
    private bool _isShopManagementPageActive;

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

    [ObservableProperty]
    private bool _isAccessControlActive;

    [ObservableProperty]
    private string _profileName;
    #endregion

    public PageViewModel? CurrentPage => _navigation.CurrentPage;

    private readonly INavigationService _navigation;
    private readonly IWindowService _windowService;
    private readonly IAuthService _authService;
    private readonly INotificationService _notificationService;
    private readonly ILoadingService _loadingService;

    //Properties that allows using services in view
    public NotificationService NotificationService
    {
        get
        {
            if (_notificationService is not NotificationService service)
                return new();

            return service;
        }
    }
    public LoadingService LoadingService
    {
        get
        {
            if (_loadingService is not LoadingService service)
                return new();

            return service;
        }
    }

    private void SetActive(ApplicationPageNames page)
    {
        IsDashboardPageActive = page == ApplicationPageNames.Dashboard;
        IsEmployeesPageActive = page == ApplicationPageNames.Employees;
        IsRolesPageActive = page == ApplicationPageNames.Roles;
        IsPermissionsPageActive = page == ApplicationPageNames.Permissions;
        IsShopManagementPageActive = page == ApplicationPageNames.ShopManagement;
        IsPortalUsersPageActive = page == ApplicationPageNames.PortalUsers;
        IsRentalsPageActive = page == ApplicationPageNames.Rentals;
        IsCompanyPageActive = page == ApplicationPageNames.CompanyInfo;
        IsOrdersPageActive = page == ApplicationPageNames.Orders;
        IsProfilePageActive = page == ApplicationPageNames.Profile;
        IsAccessControlActive = page == ApplicationPageNames.AccessControl;
    }

    public MainWindowViewModel(
        INavigationService navigation,
        IWindowService windowService,
        IAuthService authService,
        INotificationService notificationService,
        ILoadingService loadingService,
        ISessionService sessionService)
    {
        _navigation = navigation;
        _windowService = windowService;
        _authService = authService;
        _notificationService = notificationService;
        _loadingService = loadingService;
        _navigation.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(INavigationService.CurrentPage))
            {
                OnPropertyChanged(nameof(CurrentPage));
                SetActive(_navigation.CurrentPage!.PageName);
            }
        };
        _ = GoToDashboard();
        ProfileName = string.Join(' ', sessionService.UserFirstName, sessionService.UserLastName);
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
    private void GoToAccessControl() => _navigation.NavigateTo(ApplicationPageNames.AccessControl);


    [RelayCommand]
    private async Task GoToShopManagement() => await _navigation.NavigateToAsync(ApplicationPageNames.ShopManagement);

    [RelayCommand]
    private void LogOut()
    {
        _authService.Logout();
        _windowService.ShowWindow(ApplicationWindowNames.Login);
    }

    [RelayCommand]
    private void Shutdown()
    {
        _authService.Logout();
        _windowService.ShutdownApplication();
    }

    public void Dispose()
    {
       _navigation.PropertyChanged -= (s, e) =>
       {   
           if (e.PropertyName == nameof(INavigationService.CurrentPage))
           {
               OnPropertyChanged(nameof(CurrentPage));
               SetActive(_navigation.CurrentPage!.PageName);
           }
       };
       GC.SuppressFinalize(this);
    }
}
