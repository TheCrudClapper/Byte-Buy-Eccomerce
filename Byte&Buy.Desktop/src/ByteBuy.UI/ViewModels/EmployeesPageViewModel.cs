using System;
using System.Collections.ObjectModel;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel : PageViewModel
{
    public ObservableCollection<EmployeeResponse> Employees { get; set; } = [];
    private readonly MainWindowViewModel _main;
    private readonly PageFactory _pageFactory;

    public EmployeesPageViewModel(
        MainWindowViewModel main,
        PageFactory pageFactory)
    {
        PageName = ApplicationPageNames.Employees;
        _pageFactory = pageFactory;
        _main = main;
    }

    [RelayCommand]
    private void OpenEmployeePage()
        => _main.CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Employee);

}