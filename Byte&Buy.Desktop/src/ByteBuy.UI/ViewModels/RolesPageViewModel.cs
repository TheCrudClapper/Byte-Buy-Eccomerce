using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.Services;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public class RolesPageViewModel(
    AlertViewModel alert,
    MainWindowViewModel main,
    PageFactory pageFactory,
    RoleService roleService)
    : ViewModelMany<RoleResponse>(alert, main, pageFactory)
{
    
    protected override async Task LoadData()
    {
        var result = await roleService.GetAll();
        if(!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);

        Items = new ObservableCollection<RoleResponse>(result.Value!);
    }

    protected override Task Delete()
        => throw new NotImplementedException();

    protected override void OpenAddPage()
        => _main.CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Role);

}