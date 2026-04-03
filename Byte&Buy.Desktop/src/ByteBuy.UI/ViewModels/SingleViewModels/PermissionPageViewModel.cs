using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.SingleViewModels;

public partial class PermissionPageViewModel : ViewModelSingle
{
    #region MVVM Properties

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    #endregion

    private readonly IPermissionService _permissionService;
    public PermissionPageViewModel(IPermissionService permissionService, AlertViewModel alert) 
        : base(alert)
    {
        _permissionService = permissionService;
    }

    protected override async Task AddAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        var result = await _permissionService.AddAsync(new PermissionAddRequest(Name,Description));
        HandleResult(result, "Permission added successfully !");
    }

    public async Task InitializeForEditAsync(Guid permissionId)
    {
        EditingItemId = permissionId;
        IsEditMode = true;

        var result = await _permissionService.GetByIdAsync(permissionId);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        Name = value.Name;
        Description = value.Description;
    }

    protected override void Clear()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    protected override async Task UpdateAsync()
    {
        ValidateAllProperties();
        if (HasErrors || EditingItemId is null)
            return;

        var result = await _permissionService.UpdateAsync(EditingItemId.Value,
            new PermissionUpdateRequest(Name, Description));

        HandleResult(result, "Successfully updated permission");
    }
}
