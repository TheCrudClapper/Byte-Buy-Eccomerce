using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RentalSpecification
{
    /// <summary>
    /// Specification gets all rentals for seller/lender where lender is a private person
    /// </summary>
    public sealed class UserRentalLenderSpec : Specification<Rental, RentalLenderResponse>
    {
        public UserRentalLenderSpec(Guid sellerId)
        {
            Query.AsNoTracking()
                .Where(r => r.Lender.SellerId == sellerId && r.Lender.Type == SellerType.PrivatePerson)
                .Select(RentalMappings.RentalLenderResponseProjection);
        }
    }

    /// <summary>
    /// Specification get one singular rental that was lended by company
    /// </summary>
    public sealed class CompanyRentalLenderSpec : Specification<Rental, RentalLenderResponse>
    {
        public CompanyRentalLenderSpec(Guid companyId, Guid rentalId)
        {
            Query.AsNoTracking()
                .Where(r => r.Id == rentalId && (r.Lender.Type == SellerType.Company && r.Lender.SellerId == companyId))
                .Select(RentalMappings.RentalLenderResponseProjection);
        }
    }

    /// <summary>
    /// Specification gets a list of user rental (which he purchased)
    /// </summary>
    public sealed class UserRentalBorrowerSpec : Specification<Rental, UserRentalBorrowerResponse>
    {
        public UserRentalBorrowerSpec(Guid borrowerId)
        {
            Query.AsNoTracking()
                .Where(r => r.BorrowerId == borrowerId)
                .Select(RentalMappings.UserRentalBorrowerResponseProjection);
        }
    }

    /// <summary>
    /// Specification gets a list of rentals where company is lender
    /// </summary>
    public sealed class CompanyRentalListLenderSpec : Specification<Rental, CompanyRentalLenderListResponse>
    {
        public CompanyRentalListLenderSpec(Guid companyId)
        {
            Query.AsNoTracking()
                .Where(r => r.Lender.SellerId == companyId && r.Lender.Type == SellerType.Company)
                .Select(RentalMappings.CompanyRentalLenderResponseProjection);
        }
    }

}
