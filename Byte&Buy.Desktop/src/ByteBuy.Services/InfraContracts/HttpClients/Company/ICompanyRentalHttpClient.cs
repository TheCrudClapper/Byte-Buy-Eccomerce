using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.DTO.Rental;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyRentalHttpClient
{
    Task<Result<PagedList<CompanyRentalLenderListResponse>>> GetCompanyRentalsListAsync(RentalListQuery query);
    Task<Result<RentalLenderResponse>> GetCompanyRentalAsync(Guid rentalId);
}
