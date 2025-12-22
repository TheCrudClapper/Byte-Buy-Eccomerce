using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.RentOffer;
using ByteBuy.UI.ModelsUI.SaleOffer;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
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

    public override async Task LoadData()
    {
        var result = await Service.GetList();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((u, index) => u.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<SaleOfferListItem>(list);
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
