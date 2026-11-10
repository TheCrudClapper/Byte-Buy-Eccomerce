using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.SingleViewModels;

public partial class PermissionPageViewModel : ViewModelSingle
{
    #region MVVM Properties

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Name of permission is required")]
    [MaxLength(100, ErrorMessage = "Name can be at most 100 characters")]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(100, ErrorMessage = "Description can be at most 100 characters")]
    private string _description = string.Empty;

    #endregion

    private readonly IPermissionService _permissionService;
    private readonly INotificationService _notificationService;
    public PermissionPageViewModel(IPermissionService permissionService, INotificationService notificationService)
    {
        _permissionService = permissionService;
        _notificationService = notificationService;
    }

    protected override async Task AddAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        var result = await _permissionService.AddAsync(new PermissionAddRequest(Name, Description));
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while adding the permission.");
            return;
        }

        _notificationService.ShowSuccess("Permission added successfully !");
    }

    public async Task InitializeForEditAsync(Guid permissionId)
    {
        EditingItemId = permissionId;
        IsEditMode = true;

        var result = await _permissionService.GetByIdAsync(permissionId);
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while loading the permission.");
            return;
        }

        Name = result.Value.Name;
        Description = result.Value.Description;
    }

    protected override void Clear()
    {
        Name = string.Empty;
        Description = string.Empty;
        ValidateAllProperties();
    }

    protected override async Task UpdateAsync()
    {
        ValidateAllProperties();
        if (HasErrors || EditingItemId is null)
            return;

        var result = await _permissionService.UpdateAsync(EditingItemId.Value,
            new PermissionUpdateRequest(Name, Description));

        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while updating the permission.");
            return;
        }

        _notificationService.ShowSuccess("Successfully updated permission");
    }
}
