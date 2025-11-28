using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ModelsUI.Employee;

namespace ByteBuy.Services.Mappings;

public static class RoleMappings
{
    public static RoleListItem ToRoleListItem(this RoleResponse roleResponse)
    {
        return new RoleListItem
        {
            Id =  roleResponse.Id,
            Name = roleResponse.Name,
        };
    }
}