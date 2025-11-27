using ByteBuy.Services.ModelsUI.Employee;
using ByteBuy.Services.Services;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel(
    MainWindowViewModel main,
    PageFactory pageFactory,
    AlertViewModel alert, 
    EmployeeService employeeService)
    : ViewModelMany<EmployeeListItem>(alert, main, pageFactory)
{
    protected override async Task LoadData()
    {
        var result = await employeeService.GetList();
        if (!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);
        Items = new ObservableCollection<EmployeeListItem>(result.Value!);
    }
    
    protected override async Task Delete(EmployeeListItem employee)
    {
        var result = await employeeService.DeleteById(employee.Id);
        if (!result.Success)
        {
            await Alert.Show(AlertType.Error, result.Error!.Description);
            return;
        }
        
        Items.Remove(employee);
        await Alert.Show(AlertType.Success, "Employee deleted successfully");
    }
    
    protected override async Task Edit(EmployeeListItem employee)
    {
        var vm = PageFactory.GetPageViewModel(ApplicationPageNames.Employee)
            as EmployeePageViewModel;

        await vm!.InitializeForEdit(employee.Id);
        Main.CurrentPage = vm;
    }
   
    protected override void OpenAddPage()
        => Main.CurrentPage = PageFactory.GetPageViewModel(ApplicationPageNames.Employee);
    
}