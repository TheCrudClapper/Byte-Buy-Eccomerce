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

public class RolesPageViewModel : ViewModelMany<RoleListItem>
{
    private readonly IRoleService _roleService;

    public RolesPageViewModel(
        AlertViewModel alert,
        INavigationService navigationService,
        IRoleService roleService) : base(alert, navigationService)
    {
        _roleService = roleService;
        _ = LoadData();
    }

    protected override async Task Delete(RoleListItem item)
    {
        var result = await _roleService.DeleteById(item.Id);
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        Items.Remove(item);
        await Alert.ShowSuccessAlert("Role deleted successfully");
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
        var result = await _roleService.GetAll();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result.Value!
            .Select((r, index) => r.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<RoleListItem>(list);
    }

    protected override Task OpenAddPage()
    {
        Navigation.NavigateTo(ApplicationPageNames.Role, vm =>
        {
            if (vm is RolePageViewModel roleVm)
                roleVm.InitializeForAdd();
        });
        return Task.CompletedTask;
    }

}