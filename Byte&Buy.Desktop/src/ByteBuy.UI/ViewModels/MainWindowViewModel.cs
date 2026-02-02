using ByteBuy.UI.Data;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class MainWindowViewModel : WindowViewModel
{
    public PageViewModel? CurrentPage => _navigation.CurrentPage;

    private readonly INavigationService _navigation;

    public Action? Logout { get; set; }

    public MainWindowViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        _navigation.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(INavigationService.CurrentPage))
                OnPropertyChanged(nameof(CurrentPage));
        };
    }


    [RelayCommand]
    private void GoToDashboard() => _navigation.NavigateTo(ApplicationPageNames.Dashboard);

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
    private void GoToSettings() => _navigation.NavigateTo(ApplicationPageNames.Settings);

    [RelayCommand]
    private async Task GoToProfile() => await _navigation.NavigateToAsync(ApplicationPageNames.Profile, async vm =>
    {
        if (vm is ProfilePageViewModel profVm)
            await profVm.LoadData();
    });

    [RelayCommand]
    private async Task GoToRoles() => await _navigation.NavigateToAsync(ApplicationPageNames.Roles, async vm =>
    {
        if (vm is RolesPageViewModel rolesVm)
            await rolesVm.LoadDataAsync();
    });

    [RelayCommand]
    private async Task GoToAdministration() => await _navigation.NavigateToAsync(ApplicationPageNames.Administration);

    [RelayCommand]
    private void LogOut() => Logout?.Invoke();
}
