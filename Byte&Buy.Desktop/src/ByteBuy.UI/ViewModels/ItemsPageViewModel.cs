using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ModelsUI.Items;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class ItemsPageViewModel : ViewModelMany<ItemListItem, IItemService>
{
    private bool _isLoaded;

    public ItemsPageViewModel(AlertViewModel alert, INavigationService navigation,
        IDialogService dialogNavigation,
        IItemService service) : base(alert, navigation, dialogNavigation, service)
    {
    }

    protected override Task Add()
    {
        Navigation.NavigateTo(Data.ApplicationPageNames.Item, vm =>
        {
            if (vm is ItemPageViewModel itemVm)
                itemVm.InitializeForAdd();
        });
        return Task.CompletedTask;
    }

    protected override Task Edit(ItemListItem item)
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
