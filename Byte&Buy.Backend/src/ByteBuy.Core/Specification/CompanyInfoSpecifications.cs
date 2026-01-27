using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Company;
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
}
