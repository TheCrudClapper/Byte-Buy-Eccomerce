using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Category;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class CategoriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogNavigationService dialogNavigation,
    ICategoryService categoryService)
    : ViewModelMany<CategoryListItem, ICategoryService>(alert, navigation, dialogNavigation, categoryService)
{
    private bool _isLoaded;

    protected override async Task Edit(CategoryListItem item)
    {
        var result = await DialogNavigation
             .OpenDialogAsync(ApplicationDialogNames.Category, async vm =>
             {
                 if (vm is CategoryDialogViewModel categoryVm)
                     await categoryVm.InitializeForEdit(item.Id);
             });

        if (result is bool ok && ok)
            _ = LoadData();
    }

    protected override async Task LoadData()
    {
        var result = await Service.GetList();
        if (!result.Success)
        {
            Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<CategoryListItem>(list);
    }

    protected override async Task Add()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Category);

        if (result is bool ok && ok)
            _ = LoadData();
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
