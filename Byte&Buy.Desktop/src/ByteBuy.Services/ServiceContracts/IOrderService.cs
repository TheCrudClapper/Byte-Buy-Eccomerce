using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IOrderService : IBaseService
{
    Task<Result<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrderList(CancellationToken ct = default);
    Task<Result<OrderDetailsResponse>> GetOrderDetails(Guid orderId);
    Task<Result<UpdatedResponse>> DeliverOrder(Guid orderId);
    Task<Result<UpdatedResponse>> ShipOrder(Guid orderId);
}
