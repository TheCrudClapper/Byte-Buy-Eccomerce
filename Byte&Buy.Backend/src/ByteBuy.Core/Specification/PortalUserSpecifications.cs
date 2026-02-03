
using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.DTO.Internal.PortalUser;
using ByteBuy.Core.DTO.Internal.Seller;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class PortalUserSpecifications
{
    public sealed class PortalUserAggregateSpec : Specification<PortalUser>
    {
        public PortalUserAggregateSpec(Guid id)
        {
            Query.IgnoreQueryFilters()
                .Where(p => p.Id == id)
                .Include(p => p.UserPermissions)
                .Include(p => p.ShippingAddresses);
        }
    }
    public sealed class PortalUserWithAddressAndPermissionSpec : Specification<PortalUser>
    {
        public PortalUserWithAddressAndPermissionSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Include(p => p.UserPermissions)
                .Include(p => p.ShippingAddresses);
        }
    }

    public sealed class PortalUserSpec : Specification<PortalUser>
    {
        public PortalUserSpec(Guid id)
        {
            Query.Where(p => p.Id == id);
        }
    }

    public sealed class PortalUserWithPermissionsSpec : Specification<PortalUser>
    {
        public PortalUserWithPermissionsSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Include(p => p.UserPermissions);
        }
    }

    public sealed class PortalUserWithShippingAddressesSpec : Specification<PortalUser>
    {
        public PortalUserWithShippingAddressesSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Include(p => p.ShippingAddresses);
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

    public sealed class PortalUserToUserBasicInfoResponseSpec : Specification<PortalUser, UserBasicInfoResponse>
    {
        public PortalUserToUserBasicInfoResponseSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Select(PortalUserMappings.UserBasicInfoResponseProjection);
        }
    }

    public sealed class PortalUserToSellerCheckoutSpec : Specification<PortalUser, SellerCheckoutResponse>
    {
        public PortalUserToSellerCheckoutSpec(IEnumerable<Guid> sellerIds)
        {
            Query
                .Where(p => sellerIds.Contains(p.Id))
                .Select(PortalUserMappings.SellerPortalResponseProjection);
        }
    }

    public sealed class PrivateSellerSnapshotSpec : Specification<PortalUser, SellerSnapshotDto>
    {
        public PrivateSellerSnapshotSpec(IEnumerable<Guid> sellersIds)
        {
            Query
                .AsNoTracking()
                .Where(p => sellersIds.Contains(p.Id))
                .Select(PortalUserMappings.SellerSnapshotDtoProjection);
        }
    }

    public sealed class BuyserSnapshotQuerySpec : Specification<PortalUser, BuyerSnapshotQuery>
    {
        public BuyserSnapshotQuerySpec(Guid userId)
        {
            Query
                .AsNoTracking()
                .Where(p => p.Id == userId)
                .Select(PortalUserMappings.BuyerSnapshotQueryProjection);
        }
    }
}
