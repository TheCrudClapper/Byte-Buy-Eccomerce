using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RoleSpecifications
{
    public sealed class RoleToSelectListItemResponseSpec : Specification<ApplicationRole, SelectListItemResponse<Guid>>
    {
        public RoleToSelectListItemResponseSpec()
        {
            Query
                .AsNoTracking()
                .Select(RoleMappings.RoleToSelectListItemProjection);
        }
    }

    public sealed class RoleToRoleResponseSpec : Specification<ApplicationRole, RoleResponse>
    {
        public RoleToRoleResponseSpec(Guid? id = null)
        {
            Query.AsNoTracking();

            if (id is not null)
                Query.Where(i => i.Id == id);

            Query.Select(RoleMappings.RoleToRoleResponseProjection);
        }
    }

    public sealed class RoleWithRolePermissionsSpec : Specification<ApplicationRole>
    {
        public RoleWithRolePermissionsSpec(Guid id, bool ignoreQueryFilters = true)
        {
            if (ignoreQueryFilters)
                Query.IgnoreQueryFilters();


            Query.Where(r => r.Id == id)
                 .Include(r => r.RolePermissions);
        }
    }
}
