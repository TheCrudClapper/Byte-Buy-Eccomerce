using ByteBuy.UI.ModelsUI.Abstractions;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelMany<TDataGridItem> : PageViewModel
    where TDataGridItem : IListItem
{
    #region MVVM Field

    [ObservableProperty]
    private int _itemsCount;

    [ObservableProperty]
    private TDataGridItem? _selectedItem;

    [ObservableProperty]
    private ObservableCollection<TDataGridItem> _items = [];
    #endregion

    protected INavigationService Navigation;

    protected ViewModelMany(AlertViewModel alert,
        INavigationService navigation) : base(alert)
    {
        Navigation = navigation;
        _ = LoadData();
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
    protected abstract Task Delete(TDataGridItem item);

    [RelayCommand]
    protected abstract Task Edit(TDataGridItem item);

    protected abstract Task LoadData();

    [RelayCommand]
    protected abstract void OpenAddPage();
}