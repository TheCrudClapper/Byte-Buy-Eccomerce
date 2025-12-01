using ByteBuy.UI.Factories;
using ByteBuy.UI.ModelsUI.PortalUser;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class UsersPageViewModel(AlertViewModel alert, MainWindowViewModel main, PageFactory factory)
    : ViewModelMany<PortalUserListItem>(alert, main, factory)
{
    protected override Task Delete(PortalUserListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task Edit(PortalUserListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task LoadData()
    {
        throw new System.NotImplementedException();
    }

    protected override void OpenAddPage()
    {
        throw new System.NotImplementedException();
    }
}