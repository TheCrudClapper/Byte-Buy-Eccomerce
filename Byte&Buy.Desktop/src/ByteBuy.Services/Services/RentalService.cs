using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RentalService(IRentalHttpClient httpClient) : IRentalService
{
    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentalsList()
        => await httpClient.GetCompanyRentalsList();
}
