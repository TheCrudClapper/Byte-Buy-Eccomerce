using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.Mappings;
using System.Net;
namespace ByteBuy.Core.Specification;

public static class AddressSpecifications
{
    public sealed class CurrentDefaultAddressSpec : Specification<Address>
    {
        public CurrentDefaultAddressSpec(Guid userId)
        {
            Query.Where(a => a.UserId == userId && a.IsDefault);
        }
    }

    public sealed class UserAddresSpec : Specification<Address>
    {
        public UserAddresSpec(Guid userId, Guid addressId)
        {
            Query.Where(a => a.Id == addressId && a.UserId == userId);
        }
    }

    public sealed class UserAddresToDtoSpec : Specification<Address, AddressResponse>
    {
        public UserAddresToDtoSpec(Guid userId, Guid addressId)
        {
            Query.Where(a => a.Id == addressId && a.UserId == userId)
                .Select(AddressMappings.AddressDtoProjection);
        }
    }

    public sealed class AddresToDtoSpec : Specification<Address, AddressResponse>
    {
        public AddresToDtoSpec(Guid addressId)
        {
            Query.Where(a => a.Id == addressId)
                .Select(AddressMappings.AddressDtoProjection);
        }
    }

    public sealed class UserAddressesToDtoSpec : Specification<Address, AddressResponse>
    {
        public UserAddressesToDtoSpec(Guid userId)
        {
            Query
           .Where(a => a.UserId == userId)
           .Select(AddressMappings.AddressDtoProjection);
        }
    }
}