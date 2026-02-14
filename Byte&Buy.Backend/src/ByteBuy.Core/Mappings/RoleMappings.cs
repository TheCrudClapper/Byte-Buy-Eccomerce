using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.DTO.Public.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class RoleMappings
{
    public static CreatedResponse ToCreatedResponse(this ApplicationRole role)
        => new CreatedResponse(role.Id, role.DateCreated);

    public static UpdatedResponse ToUpdatedResponse(this ApplicationRole role)
        => new UpdatedResponse(role.Id, role.DateEdited!.Value);


    public static Expression<Func<ApplicationRole, SelectListItemResponse<Guid>>> SelectListItemProjection
        => r => new SelectListItemResponse<Guid>(
            r.Id,
            r.Name!);

    public static Expression<Func<ApplicationRole, RoleResponse>> RoleResponseProjection
        => r => new RoleResponse(
            r.Id,
            r.Name!,
            r.RolePermissions.Select(rp => rp.PermissionId).ToList());

    public static Expression<Func<ApplicationRole, RoleListResponse>> RoleListResponseProjection
       => r => new RoleListResponse(
           r.Id,
           r.Name!);

}
