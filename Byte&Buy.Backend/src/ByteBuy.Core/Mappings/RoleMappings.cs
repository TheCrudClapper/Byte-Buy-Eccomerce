using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;

namespace ByteBuy.Core.Mappings;

public static class RoleMappings
{
    public static SelectListItemResponse ToSelectListItemResponse(this ApplicationRole role)
    {
        return new SelectListItemResponse(role.Id, role.Name!);
    }

    public static RoleResponse ToRoleResponse(this ApplicationRole role, IEnumerable<Guid> permissionIds)
    {
        return new RoleResponse(role.Id, role.Name!, permissionIds);
    }
}
