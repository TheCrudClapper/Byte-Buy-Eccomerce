using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public class AdministrationPageViewModel : PageViewModel
{
    public DeliveriesPageViewModel DeliveriesPageViewModel { get; }
    public CountriesPageViewModel CountriesPageViewModel { get; }
    public CategoriesPageViewModel CategoriesPageViewModel { get; }

    public ConditionsPageViewModel ConditionsPageViewModel { get; }

    public AdministrationPageViewModel(AlertViewModel alert,
        DeliveriesPageViewModel deliveries,
        CountriesPageViewModel countries, 
        ConditionsPageViewModel conditions,
        CategoriesPageViewModel categories) : base(alert)
    {
        DeliveriesPageViewModel = deliveries;
        CountriesPageViewModel = countries;
        ConditionsPageViewModel = conditions;
        CategoriesPageViewModel = categories;
    }
}
