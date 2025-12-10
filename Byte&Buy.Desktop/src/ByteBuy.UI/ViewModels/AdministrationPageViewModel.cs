using Avalonia.Controls;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class AdministrationPageViewModel : PageViewModel
{
    public DeliveriesPageViewModel DeliveriesPageViewModel { get; }
    public CountriesPageViewModel CountriesPageViewModel { get; }
    public CategoriesPageViewModel CategoriesPageViewModel { get; }
    public ConditionsPageViewModel ConditionsPageViewModel { get; }

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
        DeliveriesPageViewModel deliveries,
        CountriesPageViewModel countries,
        ConditionsPageViewModel conditions,
        CategoriesPageViewModel categories) : base(alert)
    {
        DeliveriesPageViewModel = deliveries;
        CountriesPageViewModel = countries;
        ConditionsPageViewModel = conditions;
        CategoriesPageViewModel = categories;

        SelectedTab = DeliveriesPageViewModel;
        _ = DeliveriesPageViewModel.EnsureLoadedAsync();
    }

    private async Task OnSelectedTabChangedAsync(object? tab)
    {
        if (tab is TabItem tabItem)
            tab = tabItem.Content;

        switch (tab)
        {
            case DeliveriesPageViewModel deliveries:
                await deliveries.EnsureLoadedAsync();
                break;
            case CountriesPageViewModel countries:
                await countries.EnsureLoadedAsync();
                break;
            case CategoriesPageViewModel categories:
                await categories.EnsureLoadedAsync();
                break;
            case ConditionsPageViewModel conditions:
                await conditions.EnsureLoadedAsync();
                break;
        }
    }
}
