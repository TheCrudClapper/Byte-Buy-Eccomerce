using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.DTO.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class RoleMappings
{
    public static CreatedResponse ToCreatedResponse(this ApplicationRole role)
        => new CreatedResponse(role.Id, role.DateCreated);

    public static UpdatedResponse ToUpdatedResponse(this ApplicationRole role)
        => new UpdatedResponse(role.Id, role.DateEdited!.Value);


    //LINQ TO SQL
    public static Expression<Func<ApplicationRole, SelectListItemResponse<Guid>>> RoleToSelectListItemProjection
        => r => new SelectListItemResponse<Guid>(r.Id, r.Name!);

    public static Expression<Func<ApplicationRole, RoleResponse>> RoleToRoleResponseProjection
        => r => new RoleResponse(r.Id,
            r.Name!,
            r.RolePermissions.Select(rp => rp.PermissionId).ToList());
}
