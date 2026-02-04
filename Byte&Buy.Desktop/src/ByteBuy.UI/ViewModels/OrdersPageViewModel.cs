using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Order;
using ByteBuy.UI.ModelsUI.RentOffer;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class OrdersPageViewModel : ViewModelMany<OrderListItem, IOrderService>
{
    public OrdersPageViewModel(AlertViewModel alert, INavigationService navigation, IDialogService dialogNavigation, IOrderService service) 
        : base(alert, navigation, dialogNavigation, service)
    {
    }

    public override async Task LoadDataAsync()
    {
        var result = await Service.GetCompanyOrderList();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        var list = value
            .Select((u, index) => u.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<OrderListItem>(list);
    }

    [RelayCommand]
    public async Task OpenDetailsPage()
    {
        throw new System.NotImplementedException();
    }

    protected override Task AddAsync(){ throw new System.NotImplementedException();}

    protected override Task EditAsync(OrderListItem item) { throw new System.NotImplementedException(); }
}
