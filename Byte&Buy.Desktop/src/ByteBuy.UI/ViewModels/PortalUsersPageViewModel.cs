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
    private readonly IPortalUserService _userService;

    public PortalUsersPageViewModel(AlertViewModel alert,
        INavigationService navigation,
        IPortalUserService userService) : base(alert, navigation)
    {
        _userService = userService;
        _ = LoadData();
    }

    protected override async Task Delete(PortalUserListItem item)
    {
        var result = await _userService.DeleteById(item.Id);
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        Items.Remove(item);
        await Alert.ShowSuccessAlert("Successfully deleted user !");
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
        var result = await _userService.GetList();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<PortalUserListItem>(list);
    }

    protected override Task OpenAddPage()
    {
        Navigation.NavigateTo(ApplicationPageNames.PortalUser);
        return Task.CompletedTask;
    }
}
