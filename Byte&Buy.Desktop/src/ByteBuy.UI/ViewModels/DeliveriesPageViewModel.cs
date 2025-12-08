
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class DeliveriesPageViewModel : ViewModelMany<DeliveryListItem>
{
    public DeliveriesPageViewModel(AlertViewModel alert, INavigationService navigation) : base(alert, navigation)
    {
    }

    protected override Task Delete(DeliveryListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task Edit(DeliveryListItem item)
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
