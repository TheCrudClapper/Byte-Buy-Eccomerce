using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class OrderService : IOrderService
{
    public Task<Result<CreatedResponse>> AddAsync(Guid userId, OrderAddRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ReturnOrder(Guid userId, Guid orderId)
    {
        throw new NotImplementedException();
    }
}
