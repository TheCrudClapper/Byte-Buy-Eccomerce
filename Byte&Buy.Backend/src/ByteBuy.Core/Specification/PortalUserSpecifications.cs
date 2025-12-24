
using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class PortalUserSpecifications
{
    public sealed class PortalUserWithAddressAndPermissionSpec : Specification<PortalUser>
    {
        public PortalUserWithAddressAndPermissionSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Include(p => p.UserPermissions)
                .Include(p => p.Addresses);
        }
    }

    public sealed class PortalUserToPortalUserReponseSpec : Specification<PortalUser, PortalUserResponse>
    {
        public PortalUserToPortalUserReponseSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(p => p.Id == id)
                 .Select(PortalUserMappings.PortalUserResponseProjection);
        }
    }

    public sealed class PortalUserToPortalUserListResponseSpec : Specification<PortalUser, PortalUserListResponse>
    {
        public PortalUserToPortalUserListResponseSpec()
        {
            Query.AsNoTracking()
                .Select(PortalUserMappings.PortalUserListResponseProjection);
        }
    }
}
