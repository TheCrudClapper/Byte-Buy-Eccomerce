using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Order;

namespace ByteBuy.Core.ServiceContracts;

public interface IOrderCreateService
{
    Task<Result<OrderCreatedReponse>> AddAsync(Guid userId, OrderAddRequest request);
}
