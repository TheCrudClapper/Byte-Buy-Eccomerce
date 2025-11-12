using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;

namespace ByteBuy.Core.Mappings;

public static class ApplicationRoleMappings
{
    public static SelectListItemResponse ToSelectListItemResponse(this ApplicationRole role)
    {
        return new SelectListItemResponse(role.Id, role.Name!);
    }

    public static RoleResponse ToRoleResponse(this ApplicationRole role)
    {
        return new RoleResponse(role.Id, role.Name!);
    }
}
