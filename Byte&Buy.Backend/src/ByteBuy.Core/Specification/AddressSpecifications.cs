using Ardalis.Specification;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.Domain.Users.Entities;
using ByteBuy.Core.DTO.Internal.Address;
using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.Mappings;
namespace ByteBuy.Core.Specification;

public static class AddressSpecifications
{
    public sealed class UserHomeAddressSpec : Specification<PortalUser, AddressValueObject?>
    {
        public UserHomeAddressSpec(Guid userId)
        {
            Query.AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => u.HomeAddress);
        }
    }

    public sealed class UserAndShippingAddressResponseSpec : Specification<ShippingAddress, ShippingAddressResponse>
    {
        public UserAndShippingAddressResponseSpec(Guid userId, Guid addressId)
        {
            Query.AsNoTracking()
                .Where(a => a.Id == addressId && a.UserId == userId)
                .Select(AddressMappings.ShippingAddressProjection);
        }
    }

    public sealed class ShippingAddressResponseSpec : Specification<ShippingAddress, ShippingAddressResponse>
    {
        public ShippingAddressResponseSpec(Guid addressId)
        {
            Query.AsNoTracking()
                 .Where(a => a.Id == addressId)
                 .Select(AddressMappings.ShippingAddressProjection);
        }
    }

    public sealed class UserShippingAddressResponseSpec : Specification<ShippingAddress, ShippingAddressResponse>
    {
        public UserShippingAddressResponseSpec(Guid userId)
        {
            Query
               .AsNoTracking()
               .Where(a => a.UserId == userId)
               .Select(AddressMappings.ShippingAddressProjection);
        }
    }

    public sealed class UserShippingAddressListResponseSpec : Specification<ShippingAddress, ShippingAddressListResponse>
    {
        public UserShippingAddressListResponseSpec(Guid userId)
        {
            Query.Where(a => a.UserId == userId)
                .Select(AddressMappings.ShippingAddressListProjection);
        }
    }

    public sealed class UserShippingAddressCheckoutSpec : Specification<ShippingAddress, ShippingAddressCheckout>
    {
        public UserShippingAddressCheckoutSpec(Guid userId, Guid? addressId = null)
        {
            if (addressId.HasValue)
                Query.Where(a => a.Id == addressId);
            else
                Query.Where(a => a.IsDefault);

            Query.Where(a => a.UserId == userId)
                .Select(AddressMappings.ShippingAddressCheckoutProjection);
        }
    }

    public sealed class UserShippingAddressQueryModelSpec : Specification<ShippingAddress, UserShippingAddressQueryModel>
    {
        public UserShippingAddressQueryModelSpec(Guid userId, Guid addressId)
        {
            Query.AsNoTracking()
                .Where(a => a.Id == addressId && a.UserId == userId)
                .Select(AddressMappings.UserShippingAddressQueryModelProjection);
        }
    }

    public sealed class HomeAddressForOfferSpec : Specification<PortalUser, OfferAddressResponse?>
    {
        public HomeAddressForOfferSpec(Guid userId)
        {
            Query.AsNoTracking()
                 .Where(u => u.Id == userId)
                 .Select(u => u.HomeAddress == null
                    ? null
                    : new OfferAddressResponse(
                        u.HomeAddress.City,
                        u.HomeAddress.Street,
                        u.HomeAddress.PostalCode
                    ));
        }
    }
}