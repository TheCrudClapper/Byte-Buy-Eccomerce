using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.PortalUser;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class PortalUsersPageViewModel(
    AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IPortalUserService userService) : ViewModelMany<PortalUserListItem, IPortalUserService>(alert, navigation, dialogNavigation, userService)
{
    protected override async Task EditAsync(PortalUserListItem item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.PortalUser, async vm =>
        {
            if (vm is PortalUserPageViewModel userVm)
                await userVm.InitializeForEdit(item.Id);
        });
    }

    public override async Task LoadDataAsync()
    {
        var result = await Service.GetList();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<PortalUserListItem>(list);
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.PortalUser, async vm =>
        {
            if (vm is PortalUserPageViewModel userVm)
                await userVm.InitializeForAdd();
        });
    }
}
