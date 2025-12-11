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

public class RolesPageViewModel : ViewModelMany<RoleListItem, IRoleService>
{
    public RolesPageViewModel(
        AlertViewModel alert,
        INavigationService navigationService,
        IDialogNavigationService dialogNavigation,
        IRoleService roleService) : base(alert, navigationService, dialogNavigation, roleService)
    {
        _ = LoadData();
    }

    protected override async Task Edit(RoleListItem item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Role, async vm =>
        {
            if (vm is RolePageViewModel roleVm)
                await roleVm.InitializeForEdit(item.Id);
        });
    }

    protected override async Task LoadData()
    {
        var result = await Service.GetAll();
        var (ok, value) = HandleResult(result);
        if(!ok || value is null)
            return;

        var list = value
            .Select((r, index) => r.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<RoleListItem>(list);
    }

    protected override Task Add()
    {
        Navigation.NavigateTo(ApplicationPageNames.Role, vm =>
        {
            if (vm is RolePageViewModel roleVm)
                roleVm.InitializeForAdd();
        });
        return Task.CompletedTask;
    }

}