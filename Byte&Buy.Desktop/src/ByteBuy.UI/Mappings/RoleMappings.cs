using ByteBuy.Services.DTO.Role;
using ByteBuy.UI.ModelsUI.Employee;

namespace ByteBuy.UI.Mappings;

public static class RoleMappings
{
    public static RoleListItem ToListItem(this RoleResponse role, int index)
    {
        return new RoleListItem()
        {
            Id = role.Id,
            Name = role.Name,
            RowNumber = index + 1,
        };
    }
}