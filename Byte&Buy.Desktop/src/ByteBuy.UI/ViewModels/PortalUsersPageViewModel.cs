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

public class PortalUsersPageViewModel : ViewModelMany<PortalUserListItem, IPortalUserService>
{
    public PortalUsersPageViewModel(
        AlertViewModel alert,
        INavigationService navigation,
        IDialogNavigationService dialogNavigation,
        IPortalUserService userService) : base(alert, navigation, dialogNavigation, userService)
    {
        _ = LoadData();
    }

    protected override async Task Edit(PortalUserListItem item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.PortalUser, async vm =>
        {
            if (vm is PortalUserPageViewModel userVm)
                await userVm.InitializeForEdit(item.Id);
        });
    }

    protected override async Task LoadData()
    {
        var result = await Service.GetList();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<PortalUserListItem>(list);
    }

    protected override async Task Add()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.PortalUser);
    }
}
