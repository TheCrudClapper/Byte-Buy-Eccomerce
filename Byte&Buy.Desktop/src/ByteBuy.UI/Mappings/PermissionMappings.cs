using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.UI.ViewModels.Permission;

namespace ByteBuy.UI.Mappings;

public static class PermissionMappings
{
    public static PermissionManyListItem ToListItem(this PermissionResponse response, int index)
    {
        return new PermissionManyListItem()
        {
            Description = response.Description,
            Name = response.Name,
            Id = response.Id,
            RowNumber = index + 1
        };
    }
}
