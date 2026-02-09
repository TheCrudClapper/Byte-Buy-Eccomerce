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
    AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IEmployeeService service)
        : ViewModelMany<EmployeeListItem, IEmployeeService>(alert, navigation, dialogNavigation, service)
{
    public override async Task LoadDataAsync()
    {
        var result = await Service.GetList();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((e, index) => e.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<EmployeeListItem>(list);
    }

    protected override async Task EditAsync(EmployeeListItem employee)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Employee, async vm =>
        {
            if (vm is EmployeePageViewModel employeeVm)
                await employeeVm.InitializeForEdit(employee.Id);
        });
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Employee, async vm =>
        {
            if (vm is EmployeePageViewModel employeeVm)
                await employeeVm.InitializeForAdd();
        });
    }
}