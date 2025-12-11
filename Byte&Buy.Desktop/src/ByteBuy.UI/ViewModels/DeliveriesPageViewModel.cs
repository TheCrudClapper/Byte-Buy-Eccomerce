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

public class DeliveriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogNavigationService dialogNavigation,
    IDeliveryService deliveryService)
    : ViewModelMany<DeliveryListItem, IDeliveryService>(alert, navigation, dialogNavigation, deliveryService)
{
    private bool _isLoaded;
    protected override async Task Edit(DeliveryListItem item)
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Delivery, async vm =>
            {
                if (vm is DeliveryDialogViewModel deliveryVm)
                    await deliveryVm.InitializeForEdit(item.Id);
            });

        if (result is bool ok && ok)
            _ = LoadData();
    }

    protected override async Task LoadData()
    {
        var result = await Service.GetList();
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

    protected override async Task Add()
    {
        var result = await DialogNavigation
         .OpenDialogAsync(ApplicationDialogNames.Delivery);

        if (result is bool ok && ok)
            _ = LoadData();
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
