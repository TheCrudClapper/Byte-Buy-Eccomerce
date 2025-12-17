using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;

namespace ByteBuy.Core.Mappings;

public static class RoleMappings
{
    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this ApplicationRole role)
        => new SelectListItemResponse<Guid>(role.Id, role.Name!);

    public static RoleResponse ToRoleResponse(this ApplicationRole role, IEnumerable<Guid> permissionIds)
        => new RoleResponse(role.Id, role.Name!, permissionIds);

    public static CreatedResponse ToCreatedResponse(this ApplicationRole role)
        => new CreatedResponse(role.Id, role.DateCreated);

    public static UpdatedResponse ToUpdatedResponse(this ApplicationRole role)
        => new UpdatedResponse(role.Id, role.DateEdited!.Value);

}
