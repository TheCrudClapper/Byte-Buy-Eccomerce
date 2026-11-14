using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Order;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class OrdersPageViewModel : ViewModelMany<OrderListItemViewModel, IOrderService>
{
    #region Filtration fields

    [ObservableProperty]
    private string? _buyerFullName;

    [ObservableProperty]
    private string? _buyerEmail;

    [ObservableProperty]
    private DateTime? _purchasedFrom;

    [ObservableProperty]
    private DateTime? _purchasedTo;

    [ObservableProperty]
    private decimal? _totalFrom;

    [ObservableProperty]
    private decimal? _totalTo;
    #endregion

    public OrdersPageViewModel(
        INavigationService navigation,
        IDialogService dialogNavigation,
        IOrderService service,
        INotificationService notificationService,
        ILoadingService loadingService) : base(navigation, dialogNavigation, service, notificationService, loadingService)
    {
        PageName = ApplicationPageNames.Orders;
    }

    protected override async Task LoadDataCoreAsync()
    {
        var query = new OrderListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            BuyerFullName = BuyerFullName,
            BuyerEmail = BuyerEmail,
            PurchasedFrom = PurchasedFrom,
            PurchasedTo = PurchasedTo,
            TotalFrom = TotalFrom,
            TotalTo = TotalTo,
        };

        var result = await Service.GetCompanyOrderListAsync(query, Token);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading orders.");
            return;
        }

        ApplyPagination(result.Value, (u, index) => u.ToListItem(index));
    }

    [RelayCommand]
    public async Task OpenDetailsPage(OrderListItemViewModel listItem)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.OrderDetails, async vm =>
        {
            if (vm is OrderDetailsPageViewModel detailsVm)
                await detailsVm.InitializeAsync(listItem.Id);
        });
    }

    protected override Task AddAsync() { throw new System.NotImplementedException(); }

    protected override Task EditAsync(OrderListItemViewModel item) { throw new System.NotImplementedException(); }

    public override async Task ClearFiltersAsync()
    {
        BuyerFullName = null;
        BuyerEmail = null;
        PurchasedFrom = null;
        PurchasedTo = null;
        TotalFrom = null;
        TotalTo = null;
        await LoadDataAsync();
    }
}
