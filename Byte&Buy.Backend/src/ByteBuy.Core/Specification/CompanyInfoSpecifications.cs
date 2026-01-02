using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.AddressValueObj;

namespace ByteBuy.Core.Specification;

public static class CompanyInfoSpecifications
{
    public sealed class CompanyInfoToAddressValueObject : Specification<CompanyInfo, AddressValueObject>
    {
        public CompanyInfoToAddressValueObject()
        {
            Query.AsNoTracking()
                .Select(ci => ci.CompanyAddress);
        }
    }
}
