using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Country;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class CountriesPageViewModel(
    INavigationService navigation,
    IDialogService dialogNavigation,
    ICountryService countryService,
    INotificationService notificationService,
    ILoadingService loadingService)
    : ViewModelMany<CountryListItemViewModel, ICountryService>(navigation, dialogNavigation, countryService, notificationService, loadingService)
{
    #region Filtraion fields

    [ObservableProperty]
    private string? _countryName;

    [ObservableProperty]
    private string? _code;
    #endregion

    protected override async Task EditAsync(CountryListItemViewModel item)
    {
        var result = await DialogNavigation
          .OpenDialogAsync(ApplicationDialogNames.Country, async vm =>
          {
              if (vm is CountryDialogViewModel countryVm)
                  await countryVm.InitializeForEdit(item.Id);
          });

        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully updated item!");
            await LoadDataAsync();
        }
    }

    protected override async Task LoadDataCoreAsync()
    {
        var query = new CountryListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            Code = Code,
            CountryName = CountryName,
        };

        var result = await Service.GetListAsync(query, Token);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading countries.");
            return;
        }

        ApplyPagination(result.Value, (u, i) => u.ToListItem(i));
    }

    public override async Task ClearFiltersAsync()
    {
        CountryName = null;
        Code = null;
        await LoadDataAsync();
    }

    protected override async Task AddAsync()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Country);

        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully updated item!");
            await LoadDataAsync();
        }
    }
}
