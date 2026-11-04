using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Country;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class CountriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    ICountryService countryService)
    : ViewModelMany<CountryListItemViewModel, ICountryService>(alert, navigation, dialogNavigation, countryService)
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
            Alert.ShowSuccessAlert("Successfully updated item!");
            await LoadDataAsync();
        }
    }

    public override async Task LoadDataAsync()
    {
        var query = new CountryListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            Code = Code,
            CountryName = CountryName,
        };

        var result = await Service.GetListAsync(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        ApplyPagination(value, (u, i) => u.ToListItem(i));
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
            Alert.ShowSuccessAlert("Successfully updated item!");
            await LoadDataAsync();
        }
    }
}
