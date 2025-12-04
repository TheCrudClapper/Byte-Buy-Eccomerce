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

public class PortalUsersPageViewModel : ViewModelMany<PortalUserListItem>
{
    private readonly IPortalUserService _portalUserService;

    public PortalUsersPageViewModel(AlertViewModel alert,
        INavigationService navigationService,
        IPortalUserService portalUserService) : base(alert, navigationService)
    {
        _portalUserService = portalUserService;
    }

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
        var result = await _portalUserService.GetList();

        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result.Value
            .Select((item, index) => item.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<PortalUserListItem>(list);
    }

    protected override void OpenAddPage()
    {
        Navigation.NavigateTo(ApplicationPageNames.PortalUser);
    }
}