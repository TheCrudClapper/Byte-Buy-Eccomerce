using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Permission;
using ByteBuy.UI.ViewModels.Shared;
using ByteBuy.UI.ViewModels.SingleViewModels;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.ManyViewModels;

public class PermissionsPageViewModel : ViewModelMany<PermissionManyListItem, IPermissionService>
{
    public PermissionsPageViewModel(AlertViewModel alert, INavigationService navigation, IDialogService dialogNavigation, IPermissionService service) 
        : base(alert, navigation, dialogNavigation, service)
    {
        PageName = ApplicationPageNames.Permissions;
    }

    public override Task LoadDataAsync()
    {
        Items = [];
        return Task.CompletedTask;
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Permission, async vm =>
        {
            if (vm is PermissionPageViewModel permVm)
                await permVm.InitializeForAddAsync();
        });
    }

    protected override Task EditAsync(PermissionManyListItem item)
    {
        throw new System.NotImplementedException();
    }
}
