using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class RolePageViewModel : ViewModelSingle
{
    #region MVVM Fields

    [ObservableProperty][Required] private string _roleName = string.Empty;

    [ObservableProperty] private bool _isTutorialExpanded = false;

    #endregion

    public PermissionListBoxViewModel PermissionListBox { get; }
    private readonly IRoleService _roleService;

    public RolePageViewModel(AlertViewModel alert,
        PermissionListBoxViewModel permissionListBox,
        IRoleService roleService) : base(alert)
    {
        _roleService = roleService;
        PermissionListBox = permissionListBox;
    }

    public async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;

        var result = await _roleService.GetById(id);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        RoleName = value.Name;
        var permissionIds = value.PermissionIds;
        PermissionListBox.SetSelectedPermissions(permissionIds);
    }

    public override void InitializeForAdd()
    {
        base.InitializeForAdd();
        PermissionListBox.ClearSelectedPermissions();
    }

    protected override async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;

        var request = new RoleUpdateRequest(RoleName, PermissionListBox.ExtractSelectedPermissions());

        var result = await _roleService.Update(EditingItemId.Value, request);
        HandleResult(result, "Successfully updated role!");
    }

    protected override async Task AddItem()
    {
        var request = new RoleAddRequest(RoleName, PermissionListBox.ExtractSelectedPermissions());

        var result = await _roleService.Add(request);
        HandleResult(result, "Successfully added new role!");
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