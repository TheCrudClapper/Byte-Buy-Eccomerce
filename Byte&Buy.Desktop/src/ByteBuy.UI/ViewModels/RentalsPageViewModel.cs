using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Order;
using ByteBuy.UI.ModelsUI.Rental;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class RentalsPageViewModel(
    AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IRentalService service) : ViewModelMany<RentalListItem, IRentalService>(alert, navigation, dialogNavigation, service)
{
    #region Filtration fields

    [ObservableProperty]
    private string? _itemName;

    [ObservableProperty]
    private string? _borrowerEmail;

    [ObservableProperty]
    private int? _rentalDaysFrom;

    [ObservableProperty]
    private int? _rentalDaysTo;

    [ObservableProperty]
    private DateTime? _rentalStartPeriod;

    [ObservableProperty]
    private DateTime? _rentalEndPeriod;
    #endregion

    public async override Task LoadDataAsync()
    {
        var query = new RentalListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            ItemName = ItemName,
            BorrowerEmail = BorrowerEmail,
            RentalDaysFrom = RentalDaysFrom,
            RentalDaysTo = RentalDaysTo,
            RentalStartPeriod = RentalStartPeriod,
            RentalEndPeriod = RentalEndPeriod,
        };

        var result = await Service.GetCompanyRentalsList(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<RentalListItem>(
            value.Items.Select((u, index) =>
                u.ToListItem(index + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }


    [RelayCommand]
    public async Task OpenDetailsPage(RentalListItem listItem)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.RentalDetails, async vm =>
        {
            if (vm is RentalDetailsPageViewModel rentalVm)
                await rentalVm.InitializeAsync(listItem.Id);
        });
    }

    protected override Task AddAsync()
    {
        throw new System.NotImplementedException();
    }

    protected override Task EditAsync(RentalListItem item)
    {
        throw new System.NotImplementedException();
    }

    public override async Task ClearFilters()
    {
        ItemName = null;
        BorrowerEmail = null;
        RentalDaysFrom = null;
        RentalDaysTo = null;
        RentalStartPeriod = null;
        RentalEndPeriod = null;
        await LoadDataAsync();
    }
}
