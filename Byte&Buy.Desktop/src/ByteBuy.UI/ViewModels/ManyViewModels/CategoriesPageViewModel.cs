using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Category;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class CategoriesPageViewModel(
    INavigationService navigation,
    IDialogService dialogNavigation,
    ICategoryService categoryService,
    INotificationService notificationService,
    ILoadingService loadingService)
    : ViewModelMany<CategoryListItemViewModel, ICategoryService>(navigation, dialogNavigation, categoryService, notificationService, loadingService)
{
    #region Filtration fields

    [ObservableProperty]
    private string? _categoryName;
    #endregion
    protected override async Task EditAsync(CategoryListItemViewModel item)
    {
        var result = await DialogNavigation
             .OpenDialogAsync(ApplicationDialogNames.Category, async vm =>
             {
                 if (vm is CategoryDialogViewModel categoryVm)
                     await categoryVm.InitializeForEdit(item.Id);
             });

        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully updated item!");
            await LoadDataAsync();
        }
    }

    protected override async Task LoadDataCoreAsync()
    {
        var query = new CategoryListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            CategoryName = CategoryName,
        };

        var result = await Service.GetListAsync(query);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading categories.");
            return;
        }

        ApplyPagination(result.Value, (u, i) => u.ToListItem(i));
    }

    public override async Task ClearFiltersAsync()
    {
        CategoryName = null;
        await LoadDataAsync();
    }
    protected override async Task AddAsync()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Category);

        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully added new item!");
            await LoadDataAsync();
        }

    }
}
