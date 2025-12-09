using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Category;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class CategoriesPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogNavigationService dialogNavigation,
    ICategoryService categoryService)
    : ViewModelMany<CategoryListItem>(alert, navigation)
{
    protected override async Task Delete(CategoryListItem item)
    {
        var result = await categoryService.DeleteById(item.Id);
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        Items.Remove(item);
        await Alert.ShowSuccessAlert("Successfully deleted user !");
    }

    protected override Task Edit(CategoryListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override async Task LoadData()
    {
        var result = await categoryService.GetList();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<CategoryListItem>(list);
    }

    protected override void OpenAddPage()
    {
        var result = dialogNavigation.OpenDialog(
            ApplicationDialogNames.Category,
            async vm =>
            {
                await Task.CompletedTask;
            });

        if (result is true)
        {
            
        }
    }
}
