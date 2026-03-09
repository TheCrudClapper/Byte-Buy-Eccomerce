using ByteBuy.Core.Domain.Rentals;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRentalRepository : IRepositoryBase<Rental>
{
    Task<Rental?> GetUserRentalAsync(Guid userId, Guid rentalId, CancellationToken ct = default);
    Task<PagedList<CompanyRentalLenderListResponse>> GetCompanyRentalsListAsync(Guid companyId, RentalListQuery query, CancellationToken ct = default);
    Task<PagedList<UserRentalBorrowerResponse>> GetUserBorrowerRentalsAsync(UserRentalBorrowerQuery queryParams, Guid userId, CancellationToken ct = default);
    Task<PagedList<RentalLenderResponse>> GetUserLenderRentalsAsync(UserRentalLenderQuery queryParams, Guid userId, CancellationToken ct = default);
}
