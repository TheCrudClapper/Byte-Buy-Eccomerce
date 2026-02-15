using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.DTO.Internal.Company;
using ByteBuy.Core.DTO.Internal.Seller;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class CompanyInfoSpecifications
{
    public sealed class CompanyInfoToAddressWithIdSpec : Specification<Company, CompanyAddressWithId>
    {
        public CompanyInfoToAddressWithIdSpec()
        {
            Query.AsNoTracking()
                .Select(CompanyInfoMappings.CompanyAddressWithItProjection);
        }
    }
    public sealed class CompanyInfoToSellerCheckoutResponseSpec : Specification<Company, SellerCheckoutResponse>
    {
        public CompanyInfoToSellerCheckoutResponseSpec()
        {
            Query.AsNoTracking()
                .Select(CompanyInfoMappings.SellerCheckoutProjection);
        }
    }

    public sealed class CompanySellerSnapshotSpec : Specification<Company, SellerSnapshotQueryModel>
    {
        public CompanySellerSnapshotSpec()
        {
            Query.AsNoTracking()
                .Select(CompanyInfoMappings.SellerSnapshotDtoProjection);
        }
    }
}
