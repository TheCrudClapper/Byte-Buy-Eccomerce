using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Condition;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class ConditionsPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IDialogNavigationService dialogNavigation,
    IConditionService conditionService)
        : ViewModelMany<ConditionListItem>(alert, navigation)
{
    private bool _isLoaded;
    private readonly IConditionService _conditionService = conditionService;

    protected override async Task Delete(ConditionListItem item)
    {
        var decision = await dialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Confirm);

        if (decision is bool ok && ok)
        {
            var result = await _conditionService.DeleteById(item.Id);
            if (!result.Success)
            {
                await Alert.ShowErrorAlert(result.Error!.Description);
                return;
            }

            Items.Remove(item);
            await Alert.ShowSuccessAlert("Successfully deleted user !");
        }
        return;
    }

    protected override async Task Edit(ConditionListItem item)
    {
        var result = await dialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Condition, async vm =>
            {
                if (vm is ConditionDialogViewModel conditionVm)
                    await conditionVm.InitializeForEdit(item.Id);
            });

        if (result is bool ok && ok)
            _ = LoadData();
    }

    protected override async Task LoadData()
    {
        var result = await _conditionService.GetList();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<ConditionListItem>(list);
    }


    protected override async Task OpenAddPage()
    {
        var result = await dialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Condition);

        if (result is bool ok && ok)
            _ = LoadData();
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        _ = LoadData();
        _isLoaded = true;
    }
}
