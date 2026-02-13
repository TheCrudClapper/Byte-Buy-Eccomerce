using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.DTO.Public.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class CountryMappings
{
    public static SelectListItemResponse<Guid> ToSelectListItemResponse(this Country country)
        => new SelectListItemResponse<Guid>(country.Id, country.Name);

    public static CountryResponse ToCountryResponse(this Country country)
        => new CountryResponse(country.Id, country.Name, country.Code);

    public static Expression<Func<Country, CountryResponse>> CountryResponseProjection
        => c => new CountryResponse(c.Id, c.Name, c.Code);
}
