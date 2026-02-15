using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.DTO.Internal.Company;
using ByteBuy.Core.DTO.Internal.Seller;
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

    public static Expression<Func<Company, SellerCheckoutResponse>> SellerCheckoutProjection
        => c => new SellerCheckoutResponse(
            c.Id,
            c.CompanyName,
            c.Email);

    public static Expression<Func<Company, SellerSnapshotQueryModel>> SellerSnapshotDtoProjection
        => c => new SellerSnapshotQueryModel(
            c.Id,
            SellerType.Company,
            c.CompanyName,
            c.TIN,
            c.CompanyAddress);
}
