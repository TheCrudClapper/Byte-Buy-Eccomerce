using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IRentalService
{
    Task<Result<IReadOnlyCollection<UserRentalLenderResponse>>> GetSellerRentalsAsync(Guid sellerId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<UserRentalBorrowerResponse>>> GetUserRentalsAsync(Guid borrowerId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentalsListAsync(Guid sellerId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> ReturnItemToLenderAsync(Guid lenderId, Guid rentalId);
}
