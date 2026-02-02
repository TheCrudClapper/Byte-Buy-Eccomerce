using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Items;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ItemsPageViewModel : ViewModelMany<ItemListItem, IItemService>
{
    public ItemsPageViewModel(AlertViewModel alert, INavigationService navigation,
        IDialogService dialogNavigation,
        IItemService service) : base(alert, navigation, dialogNavigation, service)
    {
    }

    protected override async Task AddAsync()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Item, async vm =>
        {
            if (vm is ItemPageViewModel itemVm)
                await itemVm.InitializeForAdd();
        });
    }

    protected override async Task EditAsync(ItemListItem item)
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Item, async vm =>
        {
            if (vm is ItemPageViewModel itemVm)
                await itemVm.InitializeForEdit(item.Id);
        });
    }

    [RelayCommand]
    protected async Task Publish(ItemListItem item)
    {
        var result = await DialogNavigation.OpenDialogAsync(ApplicationDialogNames.Offer, async vm =>
        {
            if (vm is OfferDialogViewModel offerVm)
                await offerVm.InitializeForAdd(item);
        });


        if (result is bool ok && ok)
        {
            Alert.ShowSuccessAlert("Successfully updated published offer!");
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
            .Select((i, index) => i.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<ItemListItem>(list);
    }
}
