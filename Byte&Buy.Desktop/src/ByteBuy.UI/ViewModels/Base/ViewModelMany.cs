using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.ModelsUI.Abstractions;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelMany<TDataGridItem, ServiceType> : PageViewModel
    where TDataGridItem : IListItem
    where ServiceType : IBaseService
{
    #region MVVM Fields

    [ObservableProperty]
    private int _itemsCount;

    [ObservableProperty]
    private TDataGridItem? _selectedItem;

    [ObservableProperty]
    private ObservableCollection<TDataGridItem> _items = [];
    #endregion

    protected INavigationService Navigation;
    protected IDialogService DialogNavigation;
    protected readonly ServiceType Service;

    protected ViewModelMany(AlertViewModel alert,
        INavigationService navigation,
        IDialogService dialogNavigation,
        ServiceType service) : base(alert)
    {
        Navigation = navigation;
        DialogNavigation = dialogNavigation;
        Service = service;
    }

    partial void OnItemsChanged(ObservableCollection<TDataGridItem> value)
    {
        ItemsCount = Items.Count;
        UpdateRowNumbers();
    }

    //Updates Row Numbers everytime list gets updated
    private void UpdateRowNumbers()
    {
        if (Items.Count == 0) return;

        for (var i = 0; i < Items.Count; i++)
        {
            var prop = typeof(TDataGridItem).GetProperty("RowNumber");
            if (prop != null && prop.CanWrite)
                prop.SetValue(Items[i], i + 1);
        }
    }

    [RelayCommand]
    protected virtual async Task Delete(TDataGridItem item)
    {
        var decision = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Confirm);

        if (decision is bool ok && ok)
        {
            var result = await Service.DeleteById(item.Id);
            if (!result.Success)
            {
                Alert.ShowErrorAlert(result.Error!.Description);
                return;
            }

            Items.Remove(item);
            Alert.ShowSuccessAlert("Successfully deleted item !");
        }
        return;
    }

    [RelayCommand]
    protected abstract Task Edit(TDataGridItem item);

    protected abstract Task LoadData();

    [RelayCommand]
    protected abstract Task Add();
}