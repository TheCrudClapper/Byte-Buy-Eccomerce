using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Services;
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

    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    private string _roleName = string.Empty;

    [ObservableProperty]
    private bool _isTutorialExpanded = false;

    #endregion

    public PermissionListBoxViewModel PermissionListBox { get; }
    private readonly IRoleService _roleService;
    private readonly INotificationService _notificationService;

    public RolePageViewModel(
        PermissionListBoxViewModel permissionListBox,
        IRoleService roleService,
        INotificationService notificationService)
    {
        _roleService = roleService;
        PermissionListBox = permissionListBox;
        _notificationService = notificationService;
    }

    protected override async Task InitializeAsync()
        => await PermissionListBox.InitializeAsync();


    public async Task InitializeForEdit(Guid id)
    {
        IsEditMode = true;
        EditingItemId = id;
        var result = await _roleService.GetByIdAsync(id);
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while loading the role.");
            return;
        }

        await InitializeAsync();
        RoleName = result.Value.Name;
        var permissionIds = result.Value.PermissionIds;
        PermissionListBox.SetSelectedPermissions(permissionIds);
    }

    protected override async Task UpdateAsync()
    {
        if (EditingItemId is null)
            return;

        var request = new RoleUpdateRequest(RoleName, PermissionListBox.ExtractSelectedPermissions());

        var result = await _roleService.UpdateAsync(EditingItemId.Value, request);
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while updating the role.");
            return;
        }

        _notificationService.ShowSuccess("Successfully updated role!");
    }

    protected override async Task AddAsync()
    {
        var request = new RoleAddRequest(RoleName, PermissionListBox.ExtractSelectedPermissions());

        var result = await _roleService.AddAsync(request);
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while adding the role.");
            return;
        }

        _notificationService.ShowSuccess("Successfully added new role!");
    }

    protected override void Clear()
        => RoleName = string.Empty;


    [RelayCommand]
    private void ToggleTutorial()
        => IsTutorialExpanded = !IsTutorialExpanded;
}