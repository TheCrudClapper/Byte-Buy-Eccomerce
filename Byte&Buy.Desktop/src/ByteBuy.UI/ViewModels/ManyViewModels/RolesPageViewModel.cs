using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Role;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class RolesPageViewModel : ViewModelMany<RoleListItemViewModel, IRoleService>
{
    #region Filtration fields

    [ObservableProperty]
    private string? _roleName;
    #endregion

    public RolesPageViewModel(
       AlertViewModel alert,
       INavigationService navigationService,
       IDialogService dialogNavigation,
       IRoleService roleService) : base(alert, navigationService, dialogNavigation, roleService)
    {
        PageName = ApplicationPageNames.Roles;
    }

    protected override async Task EditAsync(RoleListItemViewModel item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Role, async vm =>
        {
            if (vm is RolePageViewModel roleVm)
                await roleVm.InitializeForEdit(item.Id);
        });
    }

    public override async Task LoadDataAsync()
    {
        var query = new RoleListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            RoleName = RoleName,
        };

        var result = await Service.GetListAsync(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        ApplyPagination(value, (r, index) => r.ToListItem(index));
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Role, async vm =>
        {
            if (vm is RolePageViewModel roleVm)
                await roleVm.InitializeForAddAsync();
        });
    }

    public override async Task ClearFiltersAsync()
    {
        RoleName = null;
        await LoadDataAsync();
    }
}