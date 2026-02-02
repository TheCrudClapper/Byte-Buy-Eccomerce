using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.OrderSpecifications;

namespace ByteBuy.Core.Services;

public class OrderReadService : IOrderReadService
{
    private readonly IOrderRepository _orderRepository;
    public OrderReadService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<IReadOnlyCollection<UserOrderListResponse>>> GetAllUserOrders(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserOrderListQuerySpec(userId);
        var queryResult = await _orderRepository.GetListBySpecAsync(spec, ct);

        return queryResult
            .Select(o => o.ToUserOrderListResponse())
            .ToList();
    }
}
