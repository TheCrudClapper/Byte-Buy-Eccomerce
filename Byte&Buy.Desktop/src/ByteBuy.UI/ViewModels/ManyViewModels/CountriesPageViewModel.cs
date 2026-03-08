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
using System.Collections.ObjectModel;
using System.Linq;
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

        Items = new ObservableCollection<CountryListItemViewModel>(
            value.Items.Select((u, i) =>
                u.ToListItem(i + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    public override async Task ClearFilters()
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
