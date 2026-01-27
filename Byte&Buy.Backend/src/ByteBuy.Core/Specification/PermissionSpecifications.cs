using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class PermissionSpecifications
{
    public sealed class PermissionToSelectListItemSpec : Specification<Permission, SelectListItemResponse<Guid>>
    {
        public PermissionToSelectListItemSpec()
        {
            Query
                .AsNoTracking()
                .Select(PermissionMappings.SelectListItemResponseProjection);
        }
    }
}
