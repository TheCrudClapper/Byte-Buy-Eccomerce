using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Delivery;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class DeliveriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IDeliveryService deliveryService)
    : ViewModelMany<DeliveryListItemViewModel, IDeliveryService>(alert, navigation, dialogNavigation, deliveryService)
{
    #region Filtration Fields

    [ObservableProperty]
    public string? deliveryName;

    [ObservableProperty]
    public decimal? priceFrom;

    [ObservableProperty]
    public decimal? priceTo;

    #endregion
    protected override async Task EditAsync(DeliveryListItemViewModel item)
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Delivery, async vm =>
            {
                if (vm is DeliveryDialogViewModel deliveryVm)
                    await deliveryVm.InitializeForEdit(item.Id);
            });

        if (result is bool ok && ok)
        {
            await LoadDataAsync();
            Alert.ShowSuccessAlert("Successfully updated item!");
        }
    }

    public override async Task LoadDataAsync()
    {
        var queryParams = new DeliveryListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            DeliveryName = DeliveryName,
            PriceFrom = PriceFrom,
            PriceTo = PriceTo,
        };

        var result = await Service.GetListAsync(queryParams);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<DeliveryListItemViewModel>(
            value.Items.Select((u, i) =>
                u.ToListItem(i + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        HasPreviousPage = value.Metadata.HasPrevious;
        CurrentPage = value.Metadata.CurrentPage;
    }

    public override async Task ClearFilters()
    {
        DeliveryName = null;
        PriceTo = null;
        PriceFrom = null;
        await LoadDataAsync();
    }

    protected override async Task AddAsync()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Delivery, async vm =>
            {
                if (vm is DeliveryDialogViewModel dialogVm)
                    await dialogVm.InitializeAsync();
            });

        if (result is bool ok && ok)
        {
            await LoadDataAsync();
            Alert.ShowSuccessAlert("Successfully added new item!");
        }
    }
}
