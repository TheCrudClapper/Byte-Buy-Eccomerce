using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Employee;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel
    : ViewModelMany<EmployeeListItemViewModel, IEmployeeService>
{
    #region Filtration fields

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _email;
    #endregion

    public EmployeesPageViewModel(
        AlertViewModel alert,
        INavigationService navigation,
        IDialogService dialogNavigation,
        IEmployeeService service) : base(alert, navigation, dialogNavigation, service)
    {
        PageName = ApplicationPageNames.Employees;
    }

    public override async Task LoadDataAsync()
    {
        var query = new EmployeeListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
        };

        var result = await Service.GetListAsync(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        ApplyPagination(value, (e, index) => e.ToListItem(index));
    }

    protected override async Task EditAsync(EmployeeListItemViewModel employee)
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
                await employeeVm.InitializeForAddAsync();
        });
    }

    public override async Task ClearFiltersAsync()
    {
        FirstName = null;
        LastName = null;
        Email = null;
        await LoadDataAsync();
    }
}