using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.RentOffer;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class RentOffersViewModel(
    AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IRentOfferService service)
    : ViewModelMany<RentOfferListItem, IRentOfferService>(alert, navigation, dialogNavigation, service)
{
    private bool _isLoaded;
    protected override Task Add()
    {
        throw new System.NotImplementedException();
    }

    protected override async Task Edit(RentOfferListItem item)
    {
        var result = await DialogNavigation
          .OpenDialogAsync(ApplicationDialogNames.Offer, async vm =>
          {
              if (vm is OfferDialogViewModel offerVm)
                  await offerVm.InitializeForRentEdit(item.Id);
          });

        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated offer!");
            await LoadData();
        }
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

        Items = new ObservableCollection<RentOfferListItem>(list);
    }
    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
