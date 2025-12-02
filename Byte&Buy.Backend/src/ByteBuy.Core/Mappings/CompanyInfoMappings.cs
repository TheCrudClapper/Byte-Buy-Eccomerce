using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.AddressValueObj;
using ByteBuy.Core.DTO.CompanyInfo;

namespace ByteBuy.Core.Mappings;
public static class CompanyInfoMappings
{
    public static CompanyInfoResponse ToCompanyInfoResponse(this CompanyInfo company)
    {
        return new CompanyInfoResponse(
            company.CompanyName,
            company.TIN,
            company.Email,
            company.Phone,
            company.Slogan,
            new AddressDto(
                company.Address.Street,
                company.Address.HouseNumber,
                company.Address.PostalCode,
                company.Address.City,
                company.Address.Country,
                company.Address.FlatNumber
                )
            );

    }

}
