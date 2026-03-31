using Ardalis.Specification;
using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class PermissionSpecifications
{
    public sealed class PermissionSelectListItemSpec : Specification<Permission, SelectListItemResponse<Guid>>
    {
        public PermissionSelectListItemSpec()
        {
            Query
                .AsNoTracking()
                .Select(PermissionMappings.SelectListItemResponseProjection);
        }
    }
}
