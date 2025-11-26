using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelMany<TDto> : PageViewModel where TDto: class
{
    #region MVVM Field
    
    [ObservableProperty]
    private int _itemsCount;
    
    #endregion
    
    protected readonly MainWindowViewModel _main;
    protected readonly PageFactory _pageFactory;
    
    protected ViewModelMany(AlertViewModel alert,
        MainWindowViewModel main, 
        PageFactory factory) : base(alert)
    {
        _main = main;
        _pageFactory = factory;
        _ = LoadData();
    }
    
    partial void OnItemsChanged(ObservableCollection<TDto> value)
    {
        ItemsCount = Items.Count;
    }
    
    [ObservableProperty] private ObservableCollection<TDto> _items = [];

    protected abstract Task LoadData();
    
    [RelayCommand]
    protected abstract Task Delete();

    [RelayCommand]
    protected abstract void OpenAddPage();
}