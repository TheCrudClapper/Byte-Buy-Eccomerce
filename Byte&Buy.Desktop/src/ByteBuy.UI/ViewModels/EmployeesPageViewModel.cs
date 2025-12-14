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

public partial class EmployeesPageViewModel
    : ViewModelMany<EmployeeListItem, IEmployeeService>
{
    public EmployeesPageViewModel(
        AlertViewModel alert,
        INavigationService navigation,
        IDialogService dialogNavigation,
        IEmployeeService service) : base(alert, navigation, dialogNavigation, service)
    {
        _ = LoadData();
    }

    protected override async Task LoadData()
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

    protected override async Task Edit(EmployeeListItem employee)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Employee, async vm =>
        {
            if (vm is EmployeePageViewModel employeeVm)
                await employeeVm.InitializeForEdit(employee.Id);
        });
    }

    protected override async Task Add()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Employee, async vm =>
        {
            if (vm is EmployeePageViewModel employeeVm)
                employeeVm.InitializeForAdd();
        });
    }
}