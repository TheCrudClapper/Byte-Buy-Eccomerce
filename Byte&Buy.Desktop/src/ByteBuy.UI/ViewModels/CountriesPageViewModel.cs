using ByteBuy.UI.ModelsUI.Country;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class CountriesPageViewModel : ViewModelMany<CountryListItem>
{
    public CountriesPageViewModel(AlertViewModel alert, INavigationService navigation) : base(alert, navigation)
    {
    }

    protected override Task Delete(CountryListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task Edit(CountryListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task LoadData()
    {
        throw new System.NotImplementedException();
    }

    protected override void OpenAddPage()
    {
        throw new System.NotImplementedException();
    }
}
