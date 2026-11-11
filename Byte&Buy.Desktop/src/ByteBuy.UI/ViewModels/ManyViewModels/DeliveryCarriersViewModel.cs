using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.DeliveryCarriers;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class DeliveryCarriersViewModel(
    INavigationService navigation,
    IDialogService dialogNavigation,
    IDeliveryCarrierService service,
    INotificationService notificationService,
    ILoadingService loadingService)
    : ViewModelMany<DeliveryCarrierListItemViewModel, IDeliveryCarrierService>(navigation, dialogNavigation, service, notificationService, loadingService)
{
    #region Filtration Fields

    [ObservableProperty]
    public string? deliveryCarrierName;

    [ObservableProperty]
    public string? code;
    #endregion
    protected override async Task LoadDataCoreAsync()
    {
        var query = new DeliveryCarriersListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            Code = Code,
            DeliveryCarrierName = DeliveryCarrierName
        };

        var result = await Service.GetListAsync(query);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading delivery carriers.");
            return;
        }

        ApplyPagination(result.Value, (u, i) => u.ToListItem(i));
    }

    public override async Task ClearFiltersAsync()
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
            NotificationService.ShowSuccess("Successfully updated item!");
            await LoadDataAsync();
        }
    }
    protected override async Task EditAsync(DeliveryCarrierListItemViewModel item)
    {
        var result = await DialogNavigation
         .OpenDialogAsync(ApplicationDialogNames.DeliveryCarrier, async vm =>
         {
             if (vm is DeliveryCarrierDialogViewModel carrierVm)
                 await carrierVm.InitializeForEdit(item.Id);
         });

        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully updated item!");
            await LoadDataAsync();
        }
    }
}
