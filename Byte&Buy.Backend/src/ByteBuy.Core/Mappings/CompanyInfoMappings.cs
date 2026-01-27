using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Company;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.CompanyInfo;
using System.Linq.Expressions;
namespace ByteBuy.Core.Mappings;

public static class CompanyInfoMappings
{
    public static CompanyInfoResponse ToCompanyInfoResponse(this Company company)
    {
        return new CompanyInfoResponse(
            company.CompanyName,
            company.TIN,
            company.Email,
            company.Phone,
            company.Slogan,
            new HomeAddressDto(
                company.CompanyAddress.Street,
                company.CompanyAddress.HouseNumber,
                company.CompanyAddress.PostalCity,
                company.CompanyAddress.PostalCode,
                company.CompanyAddress.City,
                company.CompanyAddress.Country,
                company.CompanyAddress.FlatNumber
                )
            );

    }

    public static Expression<Func<Company, CompanyAddressWithId>> CompanyAddressWithItProjection
        => c => new CompanyAddressWithId(
            c.Id,
            c.CompanyAddress);

}
