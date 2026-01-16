using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Specification;

public static class CompanyInfoSpecifications
{
    public sealed class CompanyInfoToAddressValueObject : Specification<Company, AddressValueObject>
    {
        public CompanyInfoToAddressValueObject()
        {
            Query.AsNoTracking()
                .Select(ci => ci.CompanyAddress);
        }
    }
}
