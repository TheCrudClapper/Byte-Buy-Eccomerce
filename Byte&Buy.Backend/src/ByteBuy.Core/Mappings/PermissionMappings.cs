using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;

namespace ByteBuy.Core.Mappings;

public static class PermissionMappings
{
    public static SelectListItemResponse ToSelectListItemResponse(this Permission permission)
    {
        return new SelectListItemResponse(permission.Id, permission.Name);
    }
}
