using ByteBuy.Services.DTO.Role;
using ByteBuy.UI.ViewModels.Role;

namespace ByteBuy.UI.Mappings;

public static class RoleMappings
{
    public static RoleListItemViewModel ToListItem(this RoleListResponse role, int index)
    {
        return new RoleListItemViewModel()
        {
            Id = role.Id,
            Name = role.Name,
            RowNumber = index,
        };
    }
}