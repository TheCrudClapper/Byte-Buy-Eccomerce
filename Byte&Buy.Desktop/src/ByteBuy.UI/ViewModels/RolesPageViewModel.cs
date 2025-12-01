using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.Services.Services;
using ByteBuy.UI.Data;
using ByteBuy.UI.Factories;
using ByteBuy.UI.ModelsUI.Employee;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;

namespace ByteBuy.UI.ViewModels;

public class RolesPageViewModel(
    AlertViewModel alert,
    MainWindowViewModel main,
    PageFactory pageFactory,
    IRoleService roleService)
    : ViewModelMany<RoleListItem>(alert, main, pageFactory)
{       
    protected override async Task Delete(RoleListItem item)
    {
        var result = await roleService.DeleteById(item.Id);
        if (!result.Success)
        {
            await Alert.Show(AlertType.Error, result.Error!.Description);
            return;
        }

        Items.Remove(item);
        await Alert.Show(AlertType.Success, "Role deleted successfully");
    }

    protected override async Task Edit(RoleListItem item)
    {
        var page = PageFactory.GetPageViewModel(ApplicationPageNames.Role)
            as RolePageViewModel;
        
        await page!.InitializeForEdit(item.Id);
        Main.CurrentPage  = page;
    }

    protected override async Task LoadData()
    {
        var result = await roleService.GetAll();
        if (!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);

        var list = result.Value.Select((r, index) => new RoleListItem
            {
                Id = r.Id,
                Name = r.Name,
                RowNumber = index + 1
            })
            .ToList();
        
        Items = new ObservableCollection<RoleListItem>(list);
    }


    protected override void OpenAddPage()
    {
        var page = PageFactory.GetPageViewModel(ApplicationPageNames.Role) as RolePageViewModel;
        page!.InitializeForAdd();
        Main.CurrentPage = page;
    }
       
}