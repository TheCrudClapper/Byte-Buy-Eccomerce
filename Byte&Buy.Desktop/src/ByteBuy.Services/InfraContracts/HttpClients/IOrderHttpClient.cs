using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IOrderHttpClient
{
    Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrdersListAsync();
    Task<Result<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId);
}
