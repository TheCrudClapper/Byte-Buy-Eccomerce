using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Order;
using ByteBuy.UI.ModelsUI.Rental;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class RentalsPageViewModel : ViewModelMany<RentalListItem, IRentalService>
{
    public RentalsPageViewModel(AlertViewModel alert, INavigationService navigation, IDialogService dialogNavigation, IRentalService service)
        : base(alert, navigation, dialogNavigation, service)
    {

    }

    public async override Task LoadDataAsync()
    {
        var result = await Service.GetCompanyRentalsList();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((u, index) => u.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<RentalListItem>(list);
    }

    protected override Task AddAsync()
    {
        throw new System.NotImplementedException();
    }

    protected override Task EditAsync(RentalListItem item)
    {
        throw new System.NotImplementedException();
    }
}
