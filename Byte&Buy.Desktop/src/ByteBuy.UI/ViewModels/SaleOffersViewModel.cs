using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.SaleOffer;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
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
    protected override async Task EditAsync(SaleOfferListItem item)
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Offer, async vm =>
            {
                if (vm is OfferDialogViewModel offerVm)
                    await offerVm.InitializeForEdit(item.Id);
            });

        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated offer!");
            await LoadDataAsync();
        }
    }

    public override async Task LoadDataAsync()
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

    //Not used
    protected override Task AddAsync()
    {
        throw new System.NotImplementedException();
    }
}
