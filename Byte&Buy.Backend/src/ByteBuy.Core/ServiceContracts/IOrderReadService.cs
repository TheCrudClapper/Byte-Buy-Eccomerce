using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOrderReadService
{
    Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetAllUserOrders(Guid userId, CancellationToken ct = default);
}
