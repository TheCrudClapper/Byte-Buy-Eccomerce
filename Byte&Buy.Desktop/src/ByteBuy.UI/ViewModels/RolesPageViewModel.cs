using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Role;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class RolesPageViewModel(
    AlertViewModel alert,
    INavigationService navigationService,
    IDialogService dialogNavigation,
    IRoleService roleService) : ViewModelMany<RoleListItem, IRoleService>(alert, navigationService, dialogNavigation, roleService)
{
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
        var result = await Service.GetAll();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((r, index) => r.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<RoleListItem>(list);
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Role, async vm =>
        {
            if (vm is RolePageViewModel roleVm)
                await roleVm.InitializeForAdd();
        });
    }

}