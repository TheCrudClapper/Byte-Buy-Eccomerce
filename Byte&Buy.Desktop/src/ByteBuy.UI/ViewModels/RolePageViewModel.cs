using System;
using System.Linq;
using System.Threading.Tasks;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.Services.Services;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels;

public partial class RolePageViewModel : ViewModelSingle
{
    #region MVVM Fields
    [ObservableProperty]
    private string _roleName = string.Empty;
    #endregion
    public PermissionListBoxViewModel PermissionListBox { get; }
    private readonly IRoleService _roleService;
    public RolePageViewModel(AlertViewModel alert,
        PermissionListBoxViewModel permissionListBox,
        IRoleService roleService) : base(alert)
    {
        _roleService = roleService; 
        PageName = ApplicationPageNames.Role;
        PermissionListBox = permissionListBox;
    }

    protected override async Task Save()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;
        
        if (IsEditMode)
        {
            await UpdateItem();
        }
        else
        {
            await AddItem();
        }
    }

    public async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;

        var result = await _roleService.GetById(id);
        if (!result.Success)
            await Alert.Show(AlertType.Error, result.Error!.Description);

        RoleName = result.Value!.Name;

        var permissionIds = result.Value.PermissionIds;
        PermissionListBox.SelectedPermission.Clear();
        
        var matchingPerms = PermissionListBox.Permissions.Where(p => permissionIds.Contains(p.Id));

        foreach (var perm in matchingPerms)
        {
            perm.IsSelected = true;
            PermissionListBox.SelectedPermission.Add(perm);
        }
    }

    public void InitializeForAdd()
    {
        EditingItemId =  Guid.Empty;
        IsEditMode = false;
        PermissionListBox.SelectedPermission.Clear();
        Clear();
    }

    private async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;

        var selectedPermissionIds = PermissionListBox.SelectedPermission.Select(p => p.Id);
        var request = new RoleUpdateRequest(RoleName, selectedPermissionIds);
        
        var response = await _roleService.Update(EditingItemId.Value, request);
        if (!response.Success)
        {
            await Alert.Show(AlertType.Error, response.Error!.Description);
            return;
        }

        await Alert.Show(AlertType.Success, "Successfully updated role");
    }

    private async Task AddItem()
    {
        var selectedPermissionIds = PermissionListBox.SelectedPermission.Select(p => p.Id);
        var request = new RoleAddRequest(RoleName, selectedPermissionIds);
        
        var response = await _roleService.Add(request);
        if (!response.Success)
            await Alert.Show(AlertType.Error, response.Error!.Description);

        await Alert.Show(AlertType.Success, "Successfully added role");
    }
    protected override void Clear()
    {
        RoleName = string.Empty;
        PermissionListBox.SelectedPermission.Clear();
    }
}