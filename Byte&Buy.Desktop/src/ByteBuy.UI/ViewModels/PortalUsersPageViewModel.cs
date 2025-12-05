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

public class PortalUsersPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IPortalUserService userService) : ViewModelMany<PortalUserListItem>(alert, navigation)
{
    private readonly IPortalUserService _userService = userService;

    protected override Task Delete(PortalUserListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task Edit(PortalUserListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override async Task LoadData()
    {
        var result = await _userService.GetList();
        if(!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index ) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<PortalUserListItem>(list);
    }

    protected override void OpenAddPage()
    {
        Navigation.NavigateTo(ApplicationPageNames.PortalUser);
    }
}
