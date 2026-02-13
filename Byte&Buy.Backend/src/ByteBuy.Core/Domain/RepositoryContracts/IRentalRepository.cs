using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.Filtration.Rental;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRentalRepository : IRepositoryBase<Rental>
{
    Task<Rental?> GetUserRental(Guid userId, Guid rentalId, CancellationToken ct = default);
    Task<PagedList<CompanyRentalLenderListResponse>> GetPagedCompanyRentalsList(
        Guid companyId, RentalListQuery query, CancellationToken ct = default);
}
