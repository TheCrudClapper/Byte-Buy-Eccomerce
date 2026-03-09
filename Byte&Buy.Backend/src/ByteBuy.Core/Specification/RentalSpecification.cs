using Ardalis.Specification;
using ByteBuy.Core.Domain.Rentals;
using ByteBuy.Core.Domain.Shared.Enums;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RentalSpecification
{
    /// <summary>
    /// Specification get one singular rental that was lended by company
    /// </summary>
    public sealed class CompanyRentalLenderSpec : Specification<Rental, RentalLenderResponse>
    {
        public CompanyRentalLenderSpec(Guid companyId, Guid rentalId)
        {
            Query.AsNoTracking()
                .Where(r => r.Id == rentalId && (r.Lender.Type == SellerType.Company && r.Lender.SellerId == companyId))
                .Select(RentalMappings.UserRentalLenderResponseProjection);
        }
    }
}
