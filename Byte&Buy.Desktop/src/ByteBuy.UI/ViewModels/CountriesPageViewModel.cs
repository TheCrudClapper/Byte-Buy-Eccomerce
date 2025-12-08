using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public class CountriesPageViewModel : ViewModelMany<>
{
    public CountriesPageViewModel(AlertViewModel alert, INavigationService navigation) : base(alert, navigation)
    {
    }
}
