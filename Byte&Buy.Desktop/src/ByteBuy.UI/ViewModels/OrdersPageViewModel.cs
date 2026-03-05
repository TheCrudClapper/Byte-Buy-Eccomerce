using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Order;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class OrdersPageViewModel(AlertViewModel alert, INavigationService navigation, IDialogService dialogNavigation, IOrderService service)
        : ViewModelMany<OrderListItemViewModel, IOrderService>(alert, navigation, dialogNavigation, service)
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

    public override async Task LoadDataAsync()
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

        var result = await Service.GetCompanyOrderList(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<OrderListItemViewModel>(
            value.Items.Select((u, index) =>
                u.ToListItem(index + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
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

    public override async Task ClearFilters()
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
