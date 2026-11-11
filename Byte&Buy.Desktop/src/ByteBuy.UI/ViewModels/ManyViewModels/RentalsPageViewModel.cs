using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Rental;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class RentalsPageViewModel : ViewModelMany<RentalListItemViewModel, IRentalService>
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

    public RentalsPageViewModel(
        INavigationService navigation,
        IDialogService dialogNavigation,
        IRentalService service,
        INotificationService notificationService,
        ILoadingService loadingService) : base(navigation, dialogNavigation, service, notificationService, loadingService)
    {
        PageName = ApplicationPageNames.Rentals;
    }

    protected override async Task LoadDataCoreAsync()
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

        var result = await Service.GetCompanyRentalsListAsync(query);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading rentals.");
            return;
        }

        ApplyPagination(result.Value, (u, index) => u.ToListItem(index));
    }


    [RelayCommand]
    public async Task OpenDetailsPage(RentalListItemViewModel listItem)
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

    protected override Task EditAsync(RentalListItemViewModel item)
    {
        throw new System.NotImplementedException();
    }

    public override async Task ClearFiltersAsync()
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
