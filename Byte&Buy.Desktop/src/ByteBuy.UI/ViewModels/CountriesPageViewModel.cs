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
    : ViewModelMany<CountryListItem>(alert, navigation)
{
    private bool _isLoaded;
    protected override async Task Delete(CountryListItem item)
    {
        var decision = await dialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Confirm);

        if (decision is bool ok && ok)
        {
            var result = await countryService.DeleteById(item.Id);
            if (!result.Success)
            {
                await Alert.ShowErrorAlert(result.Error!.Description);
                return;
            }

            Items.Remove(item);
            await Alert.ShowSuccessAlert("Successfully deleted user !");
        }
        return;
    }

    protected override async Task Edit(CountryListItem item)
    {
        var result = await dialogNavigation
          .OpenDialogAsync(ApplicationDialogNames.Country, async vm =>
          {
              if (vm is CountryDialogViewModel countryVm)
                  await countryVm.InitializeForEdit(item.Id);
          });

        if (result is bool ok && ok)
            _ = LoadData();
    }

    protected override async Task LoadData()
    {
        var result = await countryService.GetAll();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<CountryListItem>(list);
    }

    protected override async Task OpenAddPage()
    {
        var result = await dialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Country);

        if (result is bool ok && ok)
            _ = LoadData();
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        _ = LoadData();
        _isLoaded = true;
    }
}
