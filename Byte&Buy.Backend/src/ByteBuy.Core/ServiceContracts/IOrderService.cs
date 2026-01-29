using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOrderService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, OrderAddRequest request);
    Task<Result> ReturnOrder(Guid userId, Guid orderId);
}
