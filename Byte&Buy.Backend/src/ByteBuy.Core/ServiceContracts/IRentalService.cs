using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IRentalService
{
    Task<Result<IReadOnlyCollection<RentalLenderResponse>>> GetSellerRentalsAsync(Guid sellerId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<UserRentalBorrowerResponse>>> GetUserRentalsAsync(Guid borrowerId, CancellationToken ct = default);
    Task<Result<PagedList<CompanyRentalLenderListResponse>>> GetCompanyRentalsListAsync(RentalListQuery queryParams, CancellationToken ct = default);
    Task<Result<RentalLenderResponse>> GetCompanyRentalAsync(Guid rentalId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> ReturnItemToLenderAsync(Guid lenderId, Guid rentalId);
}
