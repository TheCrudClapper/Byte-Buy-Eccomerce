using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Delivery;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class DeliveriesPageViewModel(
    INavigationService navigation,
    IDialogService dialogNavigation,
    IDeliveryService deliveryService,
    INotificationService notificationService,
    ILoadingService loadingService)
    : ViewModelMany<DeliveryListItemViewModel, IDeliveryService>(navigation, dialogNavigation, deliveryService, notificationService, loadingService)
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
            NotificationService.ShowSuccess("Successfully updated item!");
        }
    }

    protected override async Task LoadDataCoreAsync()
    {
        var queryParams = new DeliveryListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            DeliveryName = DeliveryName,
            PriceFrom = PriceFrom,
            PriceTo = PriceTo,
        };

        var result = await Service.GetListAsync(queryParams, Token);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading deliveries.");
            return;
        }

        ApplyPagination(result.Value, (u, i) => u.ToListItem(i));
    }

    public override async Task ClearFiltersAsync()
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
            NotificationService.ShowSuccess("Successfully added new item!");
        }
    }
}
