using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.DTO.PortalUser;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class AddressMappings
{
    //In memory Mappings
    public static AddressResponse ToAddressResponse(this Address address)
    {
        return new AddressResponse(
            address.Id,
            address.CountryId,
            address.Label,
            address.Street,
            address.HouseNumber,
            address.PostalCity,
            address.PostalCode,
            address.City,
            address.FlatNumber,
            address.IsDefault
            );
    }

    //Ef db projections
    public static Expression<Func<Address, AddressResponse>> AddressDtoProjection
        => a => new AddressResponse(
           a.Id,
           a.CountryId,
           a.Label,
           a.Street,
           a.HouseNumber,
           a.PostalCity,
           a.PostalCode,
           a.City,
           a.FlatNumber,
           a.IsDefault
            );
}
