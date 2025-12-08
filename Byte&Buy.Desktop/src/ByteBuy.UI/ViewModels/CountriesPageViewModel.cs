using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Country;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class CountriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    ICountryService countryService)
    : ViewModelMany<CountryListItem>(alert, navigation)
{
    protected override async Task Delete(CountryListItem item)
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

    protected override Task Edit(CountryListItem item)
    {
        throw new System.NotImplementedException();
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

    protected override void OpenAddPage()
    {
        throw new System.NotImplementedException();
    }
}
