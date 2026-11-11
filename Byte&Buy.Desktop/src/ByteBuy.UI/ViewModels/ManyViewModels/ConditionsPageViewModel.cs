using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Condition;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ConditionsPageViewModel(
    INavigationService navigation,
    IDialogService dialogNavigation,
    IConditionService conditionService,
    INotificationService notificationService,
    ILoadingService loadingService)
    : ViewModelMany<ConditionListItemViewModel, IConditionService>(navigation, dialogNavigation, conditionService, notificationService, loadingService)
{
    #region Filtration Fields

    [ObservableProperty]
    public string? _conditionName;

    #endregion

    protected override async Task EditAsync(ConditionListItemViewModel item)
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Condition, async vm =>
            {
                if (vm is ConditionDialogViewModel conditionVm)
                    await conditionVm.InitializeForEdit(item.Id);
            });

        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully updated item!");
            await LoadDataAsync();
        }

    }

    protected override async Task LoadDataCoreAsync()
    {
        var query = new ConditionListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            ConditionName = ConditionName
        };

        var result = await Service.GetListAsync(query);
        if (!result.Success)
        {
            NotificationService.ShowError(result.Error?.Description ?? "An error occurred while loading conditions.");
            return;
        }

        ApplyPagination(result.Value, (u, i) => u.ToListItem(i));
    }

    public override async Task ClearFiltersAsync()
    {
        ConditionName = null;
        await LoadDataAsync();
    }

    protected override async Task AddAsync()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Condition);


        if (result is bool ok && ok)
        {
            NotificationService.ShowSuccess("Successfully added new item!");
            await LoadDataAsync();
        }
    }

}
