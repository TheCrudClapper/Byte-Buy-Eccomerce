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
public class ConditionsPageViewModel(
    AlertViewModel alert,
    INavigationService navigation,
    IDialogNavigationService dialogNavigation,
    IConditionService conditionService)
    : ViewModelMany<ConditionListItem, IConditionService>(alert, navigation, dialogNavigation, conditionService)
{
    private bool _isLoaded;

    protected override async Task Edit(ConditionListItem item)
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Condition, async vm =>
            {
                if (vm is ConditionDialogViewModel conditionVm)
                    await conditionVm.InitializeForEdit(item.Id);
            });

        if (result is bool ok && ok)
            await LoadData();
    }

    protected override async Task LoadData()
    {
        var result = await Service.GetList();
        if (!result.Success)
        {
            Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result.Value
            .Select((u, index) => u.ToListItem(index))
            .ToList();

        Items = new ObservableCollection<ConditionListItem>(list);
    }

    protected override async Task Add()
    {
        var result = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Condition);

        if (result is bool ok && ok)
            await LoadData();
    }

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
            return;

        await LoadData();
        _isLoaded = true;
    }
}
