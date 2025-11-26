using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class RolesPageViewModel : PageViewModel
{
    #region MVVM Fields

    [ObservableProperty] 
    private int _itemsCount;
    
    [ObservableProperty]
    private ObservableCollection<RoleResponse> _items = [];
    
    #endregion
    private readonly IRoleService _roleService;
    private readonly PageFactory _pageFactory;
    private readonly MainWindowViewModel _main;
    
    public RolesPageViewModel(AlertViewModel alert,
        IRoleService roleService,
        PageFactory pageFactory,
        MainWindowViewModel main
        ) : base(alert)
    {
        _pageFactory = pageFactory;
        _main = main;
        _roleService = roleService;
        PageName = ApplicationPageNames.Roles;
        _ = LoadItems();
    }


    partial void OnItemsChanged(ObservableCollection<RoleResponse> value)
    {
        ItemsCount =  Items.Count;
    }

    private async Task LoadItems()
    {
        var result = await _roleService.GetAll();
        if(!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);

        Items = new ObservableCollection<RoleResponse>(result.Value!);
    }
    
    [RelayCommand]
    private void OpenRolePage()
        => _main.CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Role);

}