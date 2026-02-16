using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Delivery;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class DeliveryCarriersViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IDeliveryCarrierService service)
    : ViewModelMany<DeliveryCarrierListItem, IDeliveryCarrierService>(alert, navigation, dialogNavigation, service)
{
    #region Filtration Fields

    [ObservableProperty]
    public string? deliveryCarrierName;

    [ObservableProperty]
    public string? code;
    #endregion
    public override async Task LoadDataAsync()
    {
        var query = new DeliveryCarriersListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            Code = Code,
            DeliveryCarrierName = DeliveryCarrierName
        };

        var result = await Service.GetList(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<DeliveryCarrierListItem>(
            value.Items.Select((u, i) =>
                u.ToListItem(i + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    public override async Task ClearFilters()
    {
        DeliveryCarrierName = null;
        Code = null;
        await LoadDataAsync();
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
