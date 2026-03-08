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
    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<CompanyRentalLenderListResponse>>> GetCompanyRentalsList(RentalListQuery query)
        => await httpClient.GetCompanyRentalsList(query);

    public async Task<Result<RentalLenderResponse>> GetCompanyRental(Guid rentalId)
        => await httpClient.GetCompanyRental(rentalId);
}
