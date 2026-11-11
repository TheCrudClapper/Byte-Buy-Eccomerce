using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Items;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ItemsPageViewModel(INavigationService navigation,
    IDialogService dialogNavigation,
    IItemService service,
    INotificationService notificationService,
    ILoadingService loadingService) : ViewModelMany<ItemListItemViewModel, IItemService>(navigation, dialogNavigation, service, notificationService, loadingService)
{
    #region Filtration fields

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private int? _stockQuantityFrom;

    [ObservableProperty]
    private int? _stockQuantityTo;
    #endregion

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Item, async vm =>
        {
            if (vm is ItemPageViewModel itemVm)
                await itemVm.InitializeForAddAsync();
        });
    }

    protected override async Task EditAsync(ItemListItemViewModel item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Item, async vm =>
        {
            if (vm is ItemPageViewModel itemVm)
                await itemVm.InitializeForEdit(item.Id);
        });
    }

    [RelayCommand]
    protected async Task Publish(ItemListItemViewModel item)
    {
        var result = await DialogNavigation.OpenDialogAsync(ApplicationDialogNames.Offer, async vm =>
        {
            if (vm is OfferDialogViewModel offerVm)
                await offerVm.InitializeForAdd(item);
        });


        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully updated published offer!");
            await LoadDataAsync();
        }
    }

    protected override async Task LoadDataCoreAsync()
    {
        var query = new ItemListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            Name = Name,
            StockQuantityFrom = StockQuantityFrom,
            StockQuantityTo = StockQuantityTo,
        };

        var result = await Service.GetListAsync(query);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading items.");
            return;
        }

        ApplyPagination(result.Value, (i, index) => i.ToListItem(index));
    }

    public override async Task ClearFiltersAsync()
    {
        Name = null;
        StockQuantityFrom = null;
        StockQuantityTo = null;
        await LoadDataAsync();
    }
}
