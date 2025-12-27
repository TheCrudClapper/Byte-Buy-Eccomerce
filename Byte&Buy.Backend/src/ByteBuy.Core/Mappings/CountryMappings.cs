using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.DTO.Shared;

namespace ByteBuy.Core.Mappings;

public static class CountryMappings
{
    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this Country country)
        => new SelectListItemResponse<Guid>(country.Id, country.Name);

    public static CountryResponse ToCountryResponse(this Country country)
        => new CountryResponse(country.Id, country.Name, country.Code);
}
