using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOrderService
{
    Task<Result<OrderCreatedReponse>> AddAsync(Guid userId, OrderAddRequest request);
    Task<Result> ReturnOrder(Guid userId, Guid orderId);
}
