using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ModelsUI.Employee;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ByteBuy.UI.Mappings;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel(
    MainWindowViewModel main,
    PageFactory pageFactory,
    AlertViewModel alert,
    IEmployeeService employeeService)
    : ViewModelMany<EmployeeListItem>(alert, main, pageFactory)
{
    protected override async Task LoadData()
    {
        var result = await employeeService.GetList();
        if (!result.Success)
        {
            await Alert.Show(AlertType.Error, result.Error!.Description);
            return;
        }
        
        var list = result.Value!
            .Select((e, index) => e.ToListItem(index))
            .ToList();
        
        Items = new ObservableCollection<EmployeeListItem>(list);
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
    {
        var vm = PageFactory.GetPageViewModel(ApplicationPageNames.Employee)
            as EmployeePageViewModel;

        vm!.InitializeForAdd();
        Main.CurrentPage = vm;
    }
}