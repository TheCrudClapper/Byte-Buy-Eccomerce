using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Country;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class CountriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    ICountryService countryService)
    : ViewModelMany<CountryListItem, ICountryService>(alert, navigation, dialogNavigation, countryService)
{
    protected override async Task EditAsync(CountryListItem item)
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
        var result = await Service.GetAll();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<CountryListItem>(list);
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
