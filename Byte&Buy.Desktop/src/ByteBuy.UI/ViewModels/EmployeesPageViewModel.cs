using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.Services;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public class EmployeesPageViewModel(
    MainWindowViewModel main,
    PageFactory pageFactory,
    AlertViewModel alert, 
    EmployeeService employeeService)
    : ViewModelMany<EmployeeResponse>(alert, main, pageFactory)
{
    protected override async Task LoadData()
    {
        var result = await employeeService.GetAll();
        if (!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);

        Items = new ObservableCollection<EmployeeResponse>(result.Value!);
    }

    protected override Task Delete()
        => throw new NotImplementedException();
    
    protected override void OpenAddPage()
        => _main.CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Employee);
    
}