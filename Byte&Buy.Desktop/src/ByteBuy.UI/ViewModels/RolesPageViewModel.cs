using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ModelsUI.Employee;
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
    : ViewModelMany<RoleListItem>(alert, main, pageFactory)
{
    protected override Task Delete(RoleListItem item)
    {
        throw new NotImplementedException();
    }

    protected override Task Edit(RoleListItem item)
    {
        throw new NotImplementedException();
    }

    protected override async Task LoadData()
    {
        // var result = await roleService.GetAll();
        // if(!result.Success)
        //     await Alert.Show(AlertType.Error, result.Error!.Description);
        //
        // Items = new ObservableCollection<RoleListItem>(result.Value!);
    }
    

    protected override void OpenAddPage()
        => Main.CurrentPage = PageFactory.GetPageViewModel(ApplicationPageNames.Role);

}