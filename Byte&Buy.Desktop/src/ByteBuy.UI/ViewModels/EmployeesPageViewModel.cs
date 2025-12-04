using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Employee;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel(
    INavigationService navigation,
    AlertViewModel alert,
    IEmployeeService employeeService)
    : ViewModelMany<EmployeeListItem>(alert, navigation)
{
    protected override async Task LoadData()
    {
        var result = await employeeService.GetList();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result.Value
            .Select((e, index) => e.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<EmployeeListItem>(list);
    }

    protected override async Task Delete(EmployeeListItem employee)
    {
        var result = await employeeService.DeleteById(employee.Id);
        if (!result.Success)
        {
            await Alert.ShowSuccessAlert(result.Error!.Description);
            return;
        }

        Items.Remove(employee);
        await Alert.ShowErrorAlert("Employee deleted successfully");
    }

    protected override async Task Edit(EmployeeListItem employee)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Employee, async vm =>
        {
            if (vm is EmployeePageViewModel employeeVm)
                await employeeVm.InitializeForEdit(employee.Id);
        });
    }

    protected override void OpenAddPage()
    {
        Navigation.NavigateTo(ApplicationPageNames.Employee, async vm =>
        {
            if (vm is EmployeePageViewModel employeeVm)
                employeeVm.InitializeForAdd();
        });
    }
}