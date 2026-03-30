using Avalonia.Controls;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.SingleViewModels;

public partial class AdministrationPageViewModel : PageViewModel
{
    public DeliveriesPageViewModel DeliveriesPageViewModel { get; }
    public CountriesPageViewModel CountriesPageViewModel { get; }
    public CategoriesPageViewModel CategoriesPageViewModel { get; }
    public ConditionsPageViewModel ConditionsPageViewModel { get; }
    public ItemsPageViewModel ItemsPageViewModel { get; }
    public RentOffersViewModel RentOffersViewModel { get; }
    public SaleOffersViewModel SaleOffersViewModel { get; }
    public DeliveryCarriersViewModel DeliveryCarriersPageViewModel { get; }

    private object? _selectedTab;
    public object? SelectedTab
    {
        get => _selectedTab;
        set
        {
            if (SetProperty(ref _selectedTab, value))
                _ = OnSelectedTabChangedAsync(value);
        }
    }

    public AdministrationPageViewModel(AlertViewModel alert,
        DeliveriesPageViewModel deliveriesViewModel,
        CountriesPageViewModel countriesViewModel,
        ConditionsPageViewModel conditionsViewModel,
        CategoriesPageViewModel categoriesViewModel,
        ItemsPageViewModel itemsPageViewModel,
        RentOffersViewModel rentOffersViewModel,
        SaleOffersViewModel saleOffersViewModel,
        DeliveryCarriersViewModel carriersViewModel) : base(alert)
    {
        PageName = ApplicationPageNames.Administration;
        DeliveriesPageViewModel = deliveriesViewModel;
        CountriesPageViewModel = countriesViewModel;
        ConditionsPageViewModel = conditionsViewModel;
        CategoriesPageViewModel = categoriesViewModel;
        DeliveryCarriersPageViewModel = carriersViewModel;
        SelectedTab = deliveriesViewModel;
        ItemsPageViewModel = itemsPageViewModel;
        RentOffersViewModel = rentOffersViewModel;
        SaleOffersViewModel = saleOffersViewModel;
    }

    private async Task OnSelectedTabChangedAsync(object? tab)
    {
        if (tab is TabItem tabItem)
            tab = tabItem.Content;

        switch (tab)
        {
            case DeliveriesPageViewModel deliveries:
                await deliveries.LoadDataAsync();
                break;
            case CountriesPageViewModel countries:
                await countries.LoadDataAsync();
                break;
            case CategoriesPageViewModel categories:
                await categories.LoadDataAsync();
                break;
            case ConditionsPageViewModel conditions:
                await conditions.LoadDataAsync();
                break;
            case ItemsPageViewModel items:
                await items.LoadDataAsync();
                break;
            case RentOffersViewModel rentOffers:
                await rentOffers.LoadDataAsync();
                break;
            case SaleOffersViewModel saleOffers:
                await saleOffers.LoadDataAsync();
                break;
            case DeliveryCarriersViewModel deliveryCarriers:
                await deliveryCarriers.LoadDataAsync();
                break;
        }
    }
}
