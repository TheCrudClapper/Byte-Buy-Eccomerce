using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ModelsUI.SaleOffer;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class SaleOffersViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    ISaleOfferService service)
    : ViewModelMany<SaleOfferListItem, ISaleOfferService>(alert, navigation, dialogNavigation, service)
{
    private bool _isLoaded;
    protected override Task Add()
    {
        throw new System.NotImplementedException();
    }

    protected override Task Edit(SaleOfferListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override Task LoadData()
    {
        throw new System.NotImplementedException();
    }
    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
