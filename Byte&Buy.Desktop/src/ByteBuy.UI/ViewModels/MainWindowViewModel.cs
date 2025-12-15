using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace ByteBuy.UI.ViewModels;

public partial class MainWindowViewModel : WindowViewModel
{
    [ObservableProperty]
    private PageViewModel _currentPage;

    private readonly PageFactory _pageFactory;
    public Action? Logout { get; set; }

    public MainWindowViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Dashboard);
    }

    [RelayCommand]
    private void GoToDashboard() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Dashboard);

    [RelayCommand]
    private void GoToCompanyDetails() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.CompanyInfo);

    [RelayCommand]
    private void GoToPortalUsers() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.PortalUsers);

    [RelayCommand]
    private void GoToEmployees() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Employees);

    [RelayCommand]
    private void GoToSettings() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Settings);

    [RelayCommand]
    private void GoToProfile() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Profile);

    [RelayCommand]
    private void GoToRoles() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Roles);

    [RelayCommand]
    private void GoToAdministration() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Administration);

    [RelayCommand]
    private void LogOut() => Logout?.Invoke();
}
