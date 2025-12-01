using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels;

public partial class RolePageViewModel : ViewModelSingle
{
    #region MVVM Fields

    [ObservableProperty] [Required] private string _roleName = string.Empty;

    [ObservableProperty] private bool _isTutorialExpanded = false;

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
        {
            await Alert.Show(AlertType.Error, result.Error!.Description);
            return;
        }
        
        RoleName = result.Value!.Name;

        var permissionIds = result.Value.PermissionIds;
        PermissionListBox.SetSelectedPermissions(permissionIds);
    }

    public void InitializeForAdd()
    {
        EditingItemId = Guid.Empty;
        IsEditMode = false;
        PermissionListBox.ClearSelectedPermissions();
        Clear();
    }

    private async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;
        
        var request = new RoleUpdateRequest(RoleName, PermissionListBox.ExtractSelectedPermissions());

        var response = await _roleService.Update(EditingItemId.Value, request);
        if (!response.Success)
            await Alert.Show(AlertType.Error, response.Error!.Description);
        else
            await Alert.Show(AlertType.Success, "Successfully added role");
    }

    private async Task AddItem()
    {
        var request = new RoleAddRequest(RoleName, PermissionListBox.ExtractSelectedPermissions());

        var response = await _roleService.Add(request);
        if (!response.Success)
            await Alert.Show(AlertType.Error, response.Error!.Description);
        else
            await Alert.Show(AlertType.Success, "Successfully added role");
        
    }

    protected override void Clear()
    {
        RoleName = string.Empty;
        PermissionListBox.ClearSelectedPermissions();
    }

    [RelayCommand]
    private void ToggleTutorial()
        => IsTutorialExpanded = !IsTutorialExpanded;
}