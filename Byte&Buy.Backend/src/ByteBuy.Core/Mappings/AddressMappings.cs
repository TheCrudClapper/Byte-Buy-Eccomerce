using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.AddressValueObj;
using ByteBuy.Core.DTO.PortalUser;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class AddressMappings
{
    //In memory Mappings
    public static ShippingAddressResponse ToAddressResponse(this ShippingAddress address)
    {
        return new ShippingAddressResponse(
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
    public static Expression<Func<ShippingAddress, ShippingAddressResponse>> AddressDtoProjection
        => a => new ShippingAddressResponse(
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

    public static HomeAddressDto ToHomeAddressDto(this AddressValueObject vo)
    {
        return new HomeAddressDto(
            vo.Street,
            vo.HouseNumber,
            vo.PostalCity,
            vo.PostalCode,
            vo.City,
            vo.Country,
            vo.FlatNumber);
    }

    public static Expression<Func<ShippingAddress, ShippingAddressListResponse>> ShippingAddressListProjection
        => a => new ShippingAddressListResponse(
            a.Id,
            a.Label,
            a.HouseNumber,
            a.PostalCity,
            a.PostalCode,
            a.FlatNumber,
            a.City,
            a.IsDefault
            );
}
