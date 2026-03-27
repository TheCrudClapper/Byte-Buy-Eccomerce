using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.ServiceContracts;

public interface IRentalService
{
    Task<Result<PagedList<RentalLenderResponse>>> GetUserLenderRentalsAsync(UserRentalLenderQuery queryParams, Guid sellerId, CancellationToken ct = default);
    Task<Result<PagedList<UserRentalBorrowerResponse>>> GetUserBorrowerRentalsAsync(UserRentalBorrowerQuery queryParams, Guid borrowerId, CancellationToken ct = default);
    Task<Result<PagedList<CompanyRentalLenderListResponse>>> GetCompanyLenderRentalsListAsync(RentalListQuery queryParams, CancellationToken ct = default);
    Task<Result<RentalLenderResponse>> GetCompanyRentalAsync(Guid rentalId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> ReturnItemToLenderAsync(Guid lenderId, Guid rentalId);
}
