using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.Mappings;
namespace ByteBuy.Core.Specification;

public static class AddressSpecifications
{
    public sealed class CurrentDefaultShippingAddressSpec : Specification<ShippingAddress>
    {
        public CurrentDefaultShippingAddressSpec(Guid userId)
        {
            Query.Where(a => a.UserId == userId && a.IsDefault);
        }
    }

    public sealed class UserShippingAddresSpec : Specification<ShippingAddress>
    {
        public UserShippingAddresSpec(Guid userId, Guid addressId)
        {
            Query.Where(a => a.Id == addressId && a.UserId == userId);
        }
    }

    public sealed class UserHomeAddressSpec : Specification<PortalUser, AddressValueObject?>
    {
        public UserHomeAddressSpec(Guid userId)
        {
            Query.AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => u.HomeAddress);
        }
    }

    public sealed class UserWithShippingAddresToDtoSpec : Specification<ShippingAddress, ShippingAddressResponse>
    {
        public UserWithShippingAddresToDtoSpec(Guid userId, Guid addressId)
        {
            Query.AsNoTracking()
                .Where(a => a.Id == addressId && a.UserId == userId)
                .Select(AddressMappings.AddressDtoProjection);
        }
    }

    public sealed class AddresToDtoSpec : Specification<ShippingAddress, ShippingAddressResponse>
    {
        public AddresToDtoSpec(Guid addressId)
        {
            Query.AsNoTracking()
                 .Where(a => a.Id == addressId)
                 .Select(AddressMappings.AddressDtoProjection);
        }
    }

    public sealed class UserAddressesToDtoSpec : Specification<ShippingAddress, ShippingAddressResponse>
    {
        public UserAddressesToDtoSpec(Guid userId)
        {
            Query
           .Where(a => a.UserId == userId)
           .AsNoTracking()
           .Select(AddressMappings.AddressDtoProjection);
        }
    }

    public sealed class UserShippingAddressToList : Specification<ShippingAddress, ShippingAddressListResponse>
    {
        public UserShippingAddressToList(Guid userId)
        {
            Query.Where(a => a.UserId == userId)
                .Select(AddressMappings.ShippingAddressListProjection);
        }
    }

    public sealed class UserShippingAddressCheckout : Specification<ShippingAddress, ShippingAddressCheckout>
    {
        public UserShippingAddressCheckout(Guid userId, Guid? addressId = null)
        {
            if (addressId.HasValue)
                Query.Where(a => a.Id == addressId);
            else
                Query.Where(a => a.IsDefault);

            Query.Where(a => a.UserId == userId)
                .Select(AddressMappings.ShippingAddressCheckoutProjection);
        }
    }
}