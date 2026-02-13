using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.DTO.Rental;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRentalService : IBaseService
{
    Task<Result<PagedList<CompanyRentalLenderListResponse>>> GetCompanyRentalsList(RentalListQuery query);
    Task<Result<RentalLenderResponse>> GetCompanyRental(Guid rentalId);
}
