using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Internal.Address;
using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class AddressMappings
{
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

    public static Expression<Func<ShippingAddress, ShippingAddressCheckout>> ShippingAddressCheckoutProjection
        => a => new ShippingAddressCheckout(
            a.Id,
            a.Label,
            a.Street,
            a.PostalCity,
            a.PostalCode,
            a.City,
            a.HouseNumber,
            a.FlatNumber,
            a.IsDefault
            );

    public static Expression<Func<ShippingAddress, UserShippingAddressQuery>> UserShippingAddressQueryProjection
        => a => new UserShippingAddressQuery(
            a.Id,
            a.Street,
            a.City,
            a.PostalCity,
            a.PostalCode,
            a.HouseNumber,
            a.FlatNumber);
}
