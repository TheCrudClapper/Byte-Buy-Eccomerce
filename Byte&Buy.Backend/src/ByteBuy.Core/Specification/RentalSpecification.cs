using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RentalSpecification
{
    //Specification gets all rentals for seller/lender
    public sealed class UserRentalLenderSpec : Specification<Rental, UserRentalLenderResponse>
    {
        public UserRentalLenderSpec(Guid sellerId)
        {
            Query.AsNoTracking()
                .Where(r => r.Lender.SellerId == sellerId && r.Lender.Type == SellerType.PrivatePerson)
                .Select(RentalMappings.UserRentalLenderResponseProjection);
        }
    }

    public sealed class UserRentalBorrowerSpec : Specification<Rental, UserRentalBorrowerResponse>
    {
        public UserRentalBorrowerSpec(Guid borrowerId)
        {
            Query.AsNoTracking()
                .Where(r => r.BorrowerId == borrowerId)
                .Select(RentalMappings.UserRentalBorrowerResponseProjection);
        }
    }

    public sealed class CompanyRentalLenderSpec : Specification<Rental, CompanyRentalLenderResponse> 
    {
        public CompanyRentalLenderSpec(Guid companyId)
        {
            Query.AsNoTracking()
                .Where(r => r.Lender.SellerId == companyId && r.Lender.Type == SellerType.Company)
                .Select(RentalMappings.CompanyRentalLenderResponseProjection);
        }
    }

}
