using System;
using System.Collections.Generic;
using System.Linq;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.UI.ViewModels.Shared;

public class PermissionGrantRevokeViewModel(IPermissionService permissionService)
    : PermissionListBoxBaseViewModel(permissionService)
{
    public override void ClearSelectedPermissions()
    {
        foreach (var perm in Permissions)
        {
            perm.IsRevoked = false;
            perm.IsGranted = false;
        }
    }

    public IEnumerable<Guid> ExtractGrantedPermissions()
        => Permissions.Where(p => p.IsGranted)
            .Select(p => p.Id)
            .ToList();

    public IEnumerable<Guid> ExtractRevokedPermissions()
        => Permissions.Where(p => p.IsRevoked)
            .Select(p => p.Id)
            .ToList();

    public void SetSelectedPermissions(IList<Guid> revokedPermissions,
        IList<Guid> grantedPermissions)
    {
        if (revokedPermissions.Any() && grantedPermissions.Any())
            return;

        foreach (var perm in Permissions)
        {
            if (revokedPermissions.Contains(perm.Id))
                perm.IsRevoked = true;
            if (grantedPermissions.Contains(perm.Id))
                perm.IsGranted = true;
        }
    }
}