using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Employee;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel(
    AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IEmployeeService service)
        : ViewModelMany<EmployeeListItemViewModel, IEmployeeService>(alert, navigation, dialogNavigation, service)
{
    #region Filtration fields

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _email;
    #endregion

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

        var result = await Service.GetList(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<EmployeeListItemViewModel>(
            value.Items.Select((e, index) =>
                e.ToListItem(index + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
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
                await employeeVm.InitializeForAdd();
        });
    }

    public override async Task ClearFilters()
    {
        FirstName = null;
        LastName = null;
        Email = null;
        await LoadDataAsync();
    }
}