using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.DTO.Public.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class PermissionMappings
{
    public static Expression<Func<Permission, SelectListItemResponse<Guid>>> SelectListItemResponseProjection
        => p => new SelectListItemResponse<Guid>(p.Id, p.Description ?? "");

    public static Expression<Func<Permission, PermissionResponse>> PermisionResponseProjection
        => p => new PermissionResponse(p.Id, p.Name, p.Description);
}
