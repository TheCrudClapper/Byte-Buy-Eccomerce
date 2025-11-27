using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.Services.ModelsUI.Abstractions;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelMany<TDataGridItem> : PageViewModel 
    where TDataGridItem: IDataGridItem
{
    #region MVVM Field
    
    [ObservableProperty]
    private int _itemsCount;
    
    [ObservableProperty]
    private TDataGridItem? _selectedItem;

    [ObservableProperty]
    private ObservableCollection<TDataGridItem> _items = [];
    #endregion

    protected readonly MainWindowViewModel Main;
    protected readonly PageFactory PageFactory;
    
    protected ViewModelMany(AlertViewModel alert,
        MainWindowViewModel main, 
        PageFactory factory) : base(alert)
    {
        Main = main;
        PageFactory = factory;
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
        if(Items.Count == 0) return;

        for(var i = 0; i < Items.Count; i++)
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