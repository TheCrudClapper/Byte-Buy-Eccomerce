using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;

namespace ByteBuy.Core.Mappings;

public static class PermissionMappings
{
    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this Permission permission)
    {
        return new SelectListItemResponse<Guid>(permission.Id, permission.Name);
    }
}
