using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Items;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ItemsPageViewModel(AlertViewModel alert, INavigationService navigation,
    IDialogService dialogNavigation,
    IItemService service) : ViewModelMany<ItemListItem, IItemService>(alert, navigation, dialogNavigation, service)
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
                await itemVm.InitializeForAdd();
        });
    }

    protected override async Task EditAsync(ItemListItem item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Item, async vm =>
        {
            if (vm is ItemPageViewModel itemVm)
                await itemVm.InitializeForEdit(item.Id);
        });
    }

    [RelayCommand]
    protected async Task Publish(ItemListItem item)
    {
        var result = await DialogNavigation.OpenDialogAsync(ApplicationDialogNames.Offer, async vm =>
        {
            if (vm is OfferDialogViewModel offerVm)
                await offerVm.InitializeForAdd(item);
        });


        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated published offer!");
            await LoadDataAsync();
        }
    }

    public override async Task LoadDataAsync()
    {
        var query = new ItemListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            Name = Name,
            StockQuantityFrom = StockQuantityFrom,
            StockQuantityTo = StockQuantityTo,
        };

        var result = await Service.GetList(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<ItemListItem>(
            value.Items.Select((i, index) =>
                i.ToListItem(index + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    public override async Task ClearFilters()
    {
        Name = null;
        StockQuantityFrom = null;
        StockQuantityTo = null;
        await LoadDataAsync();
    }
}
