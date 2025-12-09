using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ModelsUI.Condition;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using DialogHostAvalonia;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class ConditionsPageViewModel(AlertViewModel alert,
    INavigationService navigation,
    IConditionService conditionService)
    : ViewModelMany<ConditionListItem>(alert, navigation)
{
    protected override async Task Delete(ConditionListItem item)
    {
        var result = await conditionService.DeleteById(item.Id);
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        Items.Remove(item);
        await Alert.ShowSuccessAlert("Successfully deleted user !");
    }

    protected override Task Edit(ConditionListItem item)
    {
        throw new System.NotImplementedException();
    }

    protected override async Task LoadData()
    {
        var result = await conditionService.GetList();
        if (!result.Success)
        {
            await Alert.ShowErrorAlert(result.Error!.Description);
            return;
        }

        var list = result?.Value
            .Select((u, index) => u.ToListItem(index)) ?? [];

        Items = new ObservableCollection<ConditionListItem>(list);
    }

    protected override void OpenAddPage()
    {
       
    }
}
