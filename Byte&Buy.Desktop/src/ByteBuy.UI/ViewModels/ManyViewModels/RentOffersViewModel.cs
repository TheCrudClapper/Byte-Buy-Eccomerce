using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.RentOffer;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class RentOffersViewModel(
    AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IRentOfferService service)
    : ViewModelMany<RentOfferListItemViewModel, IRentOfferService>(alert, navigation, dialogNavigation, service)
{
    #region Filtration fields

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private decimal? _priceFrom;

    [ObservableProperty]
    private decimal? _priceTo;

    [ObservableProperty]
    private int? _maxRentalDaysFrom;

    [ObservableProperty]
    private int? _maxRentalDaysTo;

    [ObservableProperty]
    private int? _quantityFrom;

    [ObservableProperty]
    private int? _quantityTo;
    #endregion

    protected override async Task EditAsync(RentOfferListItemViewModel item)
    {
        var result = await DialogNavigation
          .OpenDialogAsync(ApplicationDialogNames.Offer, async vm =>
          {
              if (vm is OfferDialogViewModel offerVm)
                  await offerVm.InitializeForRentEdit(item.Id);
          });

        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated offer!");
            await LoadDataAsync();
        }
    }

    public override async Task LoadDataAsync()
    {
        var query = new RentOfferListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            Name = Name,
            PriceFrom = PriceFrom,
            PriceTo = PriceTo,
            MaxRentalDaysFrom = MaxRentalDaysFrom,
            MaxRentalDaysTo = MaxRentalDaysTo,
            QuantityFrom = QuantityFrom,
            QuantityTo = QuantityTo,
        };

        var result = await Service.GetListAsync(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<RentOfferListItemViewModel>(
            value.Items.Select((u, i) =>
                u.ToListItem(i + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    public override async Task ClearFiltersAsync()
    {
        Name = null;
        PriceFrom = null;
        PriceTo = null;
        MaxRentalDaysFrom = null;
        MaxRentalDaysTo = null;
        QuantityFrom = null;
        QuantityTo = null;
        await LoadDataAsync();
    }

    protected override async Task AddAsync()
    {
        throw new NotImplementedException();
    }
}
