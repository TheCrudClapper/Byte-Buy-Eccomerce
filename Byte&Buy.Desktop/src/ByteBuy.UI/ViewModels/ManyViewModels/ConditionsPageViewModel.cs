using ByteBuy.Services.Filtration;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Condition;
using ByteBuy.UI.ViewModels.Dialogs;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ConditionsPageViewModel(
    AlertViewModel alert,
    INavigationService navigation,
    IDialogService dialogNavigation,
    IConditionService conditionService)
    : ViewModelMany<ConditionListItemViewModel, IConditionService>(alert, navigation, dialogNavigation, conditionService)
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
            Alert.ShowSuccessAlert("Successfully updated item!");
            await LoadDataAsync();
        }

    }

    public override async Task LoadDataAsync()
    {
        var query = new ConditionListQuery
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            ConditionName = ConditionName
        };

        var result = await Service.GetListAsync(query);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Items = new ObservableCollection<ConditionListItemViewModel>(
            value.Items.Select((u, i) =>
                u.ToListItem(i + 1 + (PageNumber - 1) * PageSize)));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        CurrentPage = value.Metadata.CurrentPage;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    public override async Task ClearFilters()
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
            Alert.ShowSuccessAlert("Successfully added new item!");
            await LoadDataAsync();
        }
    }

}
