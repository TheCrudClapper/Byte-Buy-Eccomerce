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

}
