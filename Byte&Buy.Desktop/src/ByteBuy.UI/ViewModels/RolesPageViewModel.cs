using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Role;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class RolesPageViewModel(
    AlertViewModel alert,
    INavigationService navigationService,
    IDialogService dialogNavigation,
    IRoleService roleService) : ViewModelMany<RoleListItem, IRoleService>(alert, navigationService, dialogNavigation, roleService)
{
    #region Filtration fields

    [ObservableProperty]
    private string? _roleName;
    #endregion

    protected override async Task EditAsync(RoleListItem item)
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

        var result = await Service.GetList(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<RoleListItem>(
            value.Items.Select((r, index) =>
                r.ToListItem(index + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Role, async vm =>
        {
            if (vm is RolePageViewModel roleVm)
                await roleVm.InitializeForAdd();
        });
    }

    public override async Task ClearFilters()
    {
        RoleName = null;
        await LoadDataAsync();
    }
}