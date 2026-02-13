using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RoleSpecifications
{
    public sealed class SelectListItemResponseSpec : Specification<ApplicationRole, SelectListItemResponse<Guid>>
    {
        public SelectListItemResponseSpec()
        {
            Query
                .AsNoTracking()
                .Select(RoleMappings.SelectListItemProjection);
        }
    }

    public sealed class RoleResponseSpec : Specification<ApplicationRole, RoleResponse>
    {
        public RoleResponseSpec(Guid? id = null)
        {
            Query.AsNoTracking();

            if (id is not null)
                Query.Where(i => i.Id == id);

            Query.Select(RoleMappings.RoleResponseProjection);
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
