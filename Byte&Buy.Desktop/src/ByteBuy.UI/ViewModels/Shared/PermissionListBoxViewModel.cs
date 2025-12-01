using System;
using System.Collections.Generic;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ModelsUI.Permission;
using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class PermissionListBoxViewModel : ViewModelBase
{
    #region Mvvm Fields
    [ObservableProperty] private ObservableCollection<PermissionListItem> _permissions = [];
    [ObservableProperty] private ObservableCollection<PermissionListItem> _selectedPermissions = [];
    #endregion

    private readonly IPermissionService _permissionService;

    public PermissionListBoxViewModel(IPermissionService permissionService)
    {
        _permissionService = permissionService;
        _ = LoadPermissions();
    }

    private async Task LoadPermissions()
    {
        var result = await _permissionService.GetSelectList();
        if (!result.Success || result.Value is null)
            return;

        var list = result.Value.Select((p, index) => new PermissionListItem()
        {
            Id = p.Id,
            RowNumber = index + 1,
            IsSelected = false,
            Name = p.Title,
        });

        Permissions = new ObservableCollection<PermissionListItem>(list);
        foreach (var perm in Permissions)
        {
            perm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(PermissionListItem.IsSelected)) ;
                UpdateSelectedPermissions();
            };
        }
    }
    
    private void UpdateSelectedPermissions() 
        => SelectedPermissions = new ObservableCollection<PermissionListItem>(Permissions.Where(p => p.IsSelected));

    public IEnumerable<Guid> ExtractSelectedPermissions()
    {
        return SelectedPermissions
            .Where(sp => sp.IsSelected)
            .Select(sp => sp.Id)
            .ToList();
    }

    public void SetSelectedPermissions(IEnumerable<Guid>? selectedPermissions)
    {
        if (selectedPermissions is null)
            return;
        
        SelectedPermissions.Clear();
        
        var matchingPerms = Permissions
            .Where(p => selectedPermissions
                .Contains(p.Id));

        foreach (var perm in matchingPerms)
        {
            perm.IsSelected = true;
            SelectedPermissions.Add(perm);
        }
    }

    public void ClearSelectedPermissions()
    {
        SelectedPermissions.Clear();
        foreach (var perm in SelectedPermissions)
        {
            perm.IsSelected = false;
        }
    }
}