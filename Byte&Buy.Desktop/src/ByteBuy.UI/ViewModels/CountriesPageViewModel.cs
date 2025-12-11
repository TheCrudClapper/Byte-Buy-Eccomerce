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
    IDialogNavigationService dialogNavigation,
    ICountryService countryService)
    : ViewModelMany<CountryListItem, ICountryService>(alert, navigation, dialogNavigation, countryService)
{
    private bool _isLoaded;
    protected override async Task Edit(CountryListItem item)
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
            await LoadData();
        }       
    }

    protected override async Task LoadData()
    {
        var result = await Service.GetAll();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<CountryListItem>(list);
    }

    protected override async Task Add()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Country);

        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated item!");
            await LoadData();
        }      
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
