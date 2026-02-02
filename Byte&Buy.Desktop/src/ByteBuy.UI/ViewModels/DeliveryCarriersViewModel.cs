using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class DeliveryCarriersViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IDeliveryCarrierService service)
    : ViewModelMany<DeliveryCarrierListItem, IDeliveryCarrierService>(alert, navigation, dialogNavigation, service)
{
    public override async Task LoadDataAsync()
    {
        var result = await Service.GetAll();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<DeliveryCarrierListItem>(list);
    }

    protected override async Task AddAsync()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.DeliveryCarrier);

        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated item!");
            await LoadDataAsync();
        }
    }
    protected override async Task EditAsync(DeliveryCarrierListItem item)
    {
        var result = await DialogNavigation
         .OpenDialogAsync(ApplicationDialogNames.DeliveryCarrier, async vm =>
         {
             if (vm is DeliveryCarrierDialogViewModel carrierVm)
                 await carrierVm.InitializeForEdit(item.Id);
         });

        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated item!");
            await LoadDataAsync();
        }
    }
}
