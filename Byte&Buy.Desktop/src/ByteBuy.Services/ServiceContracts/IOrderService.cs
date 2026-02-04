using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IOrderService : IBaseService
{
    Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrderList(CancellationToken ct = default);
}
