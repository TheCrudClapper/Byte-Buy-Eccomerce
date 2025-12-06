using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.PortalUser;

namespace ByteBuy.Core.Mappings;

public static class AddressMappings
{
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
}
