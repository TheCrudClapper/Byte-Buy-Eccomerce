using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class PermissionMappings
{
    public static Expression<Func<Permission, SelectListItemResponse<Guid>>> SelectListItemResponseProjection
        => p => new SelectListItemResponse<Guid>(p.Id, p.Description ?? "");
}
