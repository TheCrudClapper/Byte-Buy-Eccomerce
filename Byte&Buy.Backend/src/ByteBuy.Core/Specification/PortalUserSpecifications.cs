
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
    public sealed class PortalUserAggregateAndUserPermissionSpec : Specification<PortalUser>
    {
        public PortalUserAggregateAndUserPermissionSpec(Guid id)
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

    public sealed class PortalUserWithUserPermissionsSpec : Specification<PortalUser>
    {
        public PortalUserWithUserPermissionsSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Include(p => p.UserPermissions);
        }
    }

    public sealed class PortalUserAggregateSpec : Specification<PortalUser>
    {
        public PortalUserAggregateSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Include(p => p.ShippingAddresses);
        }
    }

    public sealed class PortalUserReponseSpec : Specification<PortalUser, PortalUserResponse>
    {
        public PortalUserReponseSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(p => p.Id == id)
                 .Select(PortalUserMappings.PortalUserResponseProjection);
        }
    }

    public sealed class UserBasicInfoResponseSpec : Specification<PortalUser, UserBasicInfoResponse>
    {
        public UserBasicInfoResponseSpec(Guid id)
        {
            Query.Where(p => p.Id == id)
                .Select(PortalUserMappings.UserBasicInfoResponseProjection);
        }
    }

    public sealed class UserSellerCheckoutSpec : Specification<PortalUser, SellerCheckoutResponse>
    {
        public UserSellerCheckoutSpec(IEnumerable<Guid> sellerIds)
        {
            Query
                .Where(p => sellerIds.Contains(p.Id))
                .Select(PortalUserMappings.SellerPortalResponseProjection);
        }
    }

    public sealed class PrivateSellersSnapshotSpec : Specification<PortalUser, SellerSnapshotQueryModel>
    {
        public PrivateSellersSnapshotSpec(IEnumerable<Guid> sellersIds)
        {
            Query
                .AsNoTracking()
                .Where(p => sellersIds.Contains(p.Id))
                .Select(PortalUserMappings.SellerSnapshotDtoProjection);
        }
    }

    public sealed class BuyserSnapshotQueryModelSpec : Specification<PortalUser, PortalUserBuyerQueryModel>
    {
        public BuyserSnapshotQueryModelSpec(Guid userId)
        {
            Query
                .AsNoTracking()
                .Where(p => p.Id == userId)
                .Select(PortalUserMappings.BuyerSnapshotQueryProjection);
        }
    }
}
