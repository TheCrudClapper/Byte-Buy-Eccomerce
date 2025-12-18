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
    private bool _isLoaded;

    public ItemsPageViewModel(AlertViewModel alert, INavigationService navigation,
        IDialogService dialogNavigation,
        IItemService service) : base(alert, navigation, dialogNavigation, service)
    {
    }

    protected override async Task Add()
    {
        await Navigation.NavigateToAsync(ApplicationPageNames.Item, async vm =>
        {
            if (vm is ItemPageViewModel itemVm)
                await itemVm.InitializeForAdd();
        });
    }

    protected override async Task Edit(ItemListItem item)
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
        await DialogNavigation.OpenDialogAsync(ApplicationDialogNames.Offer, async vm =>
        {
            if (vm is OfferDialogViewModel offerVm)
                await offerVm.InitializeForAdd(item);
        });
    }

    public override async Task LoadData()
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

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
