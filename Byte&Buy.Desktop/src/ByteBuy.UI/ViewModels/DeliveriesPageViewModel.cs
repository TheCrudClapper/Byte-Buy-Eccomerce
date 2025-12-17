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
    IDialogService dialogNavigation,
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
        {
            await LoadData();
            Alert.ShowSuccessAlert("Successfully updated item!");
        }
    }

    public override async Task LoadData()
    {
        var result = await Service.GetList();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
           .Select((d, index) => d.ToListItem(index))
           .ToList();

        Items = new ObservableCollection<DeliveryListItem>(list);
    }

    protected override async Task Add()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Delivery, async vm =>
            {
                if (vm is DeliveryDialogViewModel dialogVm)
                    await dialogVm.InitializeAsync();
            });

        if (result is bool ok && ok)
        {
            await LoadData();
            Alert.ShowSuccessAlert("Successfully added new item!");
        }
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
