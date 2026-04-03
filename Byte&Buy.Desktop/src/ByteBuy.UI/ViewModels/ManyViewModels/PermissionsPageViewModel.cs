using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Permission;
using ByteBuy.UI.ViewModels.Shared;
using ByteBuy.UI.ViewModels.SingleViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.ManyViewModels;

public class PermissionsPageViewModel : ViewModelMany<PermissionManyListItem, IPermissionService>
{
    public PermissionsPageViewModel(AlertViewModel alert, INavigationService navigation, IDialogService dialogNavigation, IPermissionService service) 
        : base(alert, navigation, dialogNavigation, service)
    {
        PageName = ApplicationPageNames.Permissions;
    }

    public override async Task LoadDataAsync()
    {
        var result = await Service.GetListAsync();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<PermissionManyListItem>(value.Select((p, i) => p.ToListItem(i)));
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Permission, async vm =>
        {
            if (vm is PermissionPageViewModel permVm)
                await permVm.InitializeForAddAsync();
        });
    }

    protected override async Task EditAsync(PermissionManyListItem item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Permission, async vm =>
        {
            if (vm is PermissionPageViewModel permVm)
                await permVm.InitializeForEditAsync(item.Id);
        });
    }
}
