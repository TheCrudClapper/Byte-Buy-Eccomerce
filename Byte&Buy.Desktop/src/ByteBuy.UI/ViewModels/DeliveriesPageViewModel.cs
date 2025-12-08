using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class DeliveriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDeliveryService deliveryService)
    : ViewModelMany<DeliveryListItem>(alert, navigation)
{
    protected override async Task Delete(DeliveryListItem item)
    {
        var result = await deliveryService.DeleteById(item.Id);
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }
        Items.Remove(item);
        await Alert.ShowSuccessAlert("Successfully deleted user !");
    }

    protected override Task Edit(DeliveryListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override async Task LoadData()
    {
        var result = await deliveryService.GetList();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result.Value
           .Select((d, index) => d.ToListItem(index))
           .ToList();

        Items = new ObservableCollection<DeliveryListItem>(list);
    }

    protected override void OpenAddPage()
    {
        throw new System.NotImplementedException();
    }
}
