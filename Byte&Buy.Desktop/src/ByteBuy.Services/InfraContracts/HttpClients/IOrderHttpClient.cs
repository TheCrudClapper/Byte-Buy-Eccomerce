using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IOrderHttpClient
{
    Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrdersListAsync();
}
