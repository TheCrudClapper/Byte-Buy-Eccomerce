using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.DTO.Rental;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RentalService(ICompanyRentalHttpClient httpClient) : IRentalService
{
    public Task<Result> DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<CompanyRentalLenderListResponse>>> GetCompanyRentalsListAsync(RentalListQuery query)
        => await httpClient.GetCompanyRentalsListAsync(query);

    public async Task<Result<RentalLenderResponse>> GetCompanyRentalAsync(Guid rentalId)
        => await httpClient.GetCompanyRentalAsync(rentalId);
}
