using ByteBuy.Services.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteBuy.UI.ViewModels.Shared;

public class PermissionListBoxViewModel(IPermissionService permissionService)
    : PermissionListBoxBaseViewModel(permissionService)
{

    public IEnumerable<Guid> ExtractSelectedPermissions()
    => Permissions
        .Where(p => p.IsSelected)
        .Select(p => p.Id)
        .ToList();

    public void SetSelectedPermissions(IEnumerable<Guid>? selectedPermissions)
    {
        if (selectedPermissions is null)
            return;

        var matchingPerms = Permissions
            .Where(p => selectedPermissions
                .Contains(p.Id));

        foreach (var perm in matchingPerms)
        {
            perm.IsSelected = true;
        }
    }

    public override void ClearSelectedPermissions()
    {
        foreach (var perm in Permissions)
        {
            perm.IsSelected = false;
        }
    }
}