using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.Mappings;
namespace ByteBuy.Core.Specification;

public static class AddressSpecifications
{
    public sealed class CurrentDefaultAddressSpec : Specification<ShippingAddress>
    {
        public CurrentDefaultAddressSpec(Guid userId)
        {
            Query.Where(a => a.UserId == userId && a.IsDefault);
        }
    }

    public sealed class UserAddresSpec : Specification<ShippingAddress>
    {
        public UserAddresSpec(Guid userId, Guid addressId)
        {
            Query.Where(a => a.Id == addressId && a.UserId == userId);
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
}