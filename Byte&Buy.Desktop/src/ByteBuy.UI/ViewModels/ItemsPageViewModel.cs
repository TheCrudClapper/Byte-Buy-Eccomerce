using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ModelsUI.Items;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class ItemsPageViewModel : ViewModelMany<ItemListItem, IItemService>
{
    public ItemsPageViewModel(AlertViewModel alert, INavigationService navigation,
        IDialogNavigationService dialogNavigation,
        IItemService service) : base(alert, navigation, dialogNavigation, service)
    {
    }

    protected override Task Add()
    {
        throw new System.NotImplementedException();
    }

    protected override Task Edit(ItemListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task LoadData()
    {
        throw new System.NotImplementedException();
    }
}
