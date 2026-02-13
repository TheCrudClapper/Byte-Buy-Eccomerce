using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Internal.Order;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<IReadOnlyCollection<Order>> GetOrdersByPaymentIdAscyn(Guid userId, Guid paymentId, CancellationToken ct = default);
    Task<Order?> GetUserOrderAsync(Guid userId, Guid orderId, CancellationToken ct = default);
    Task<Order?> GetSellerOrderAsync(Guid sellerId, Guid orderId, CancellationToken ct = default);
    Task<PagedList<CompanyOrderListResponse>> GetOrdersListAsync(OrderCompanyListQuery queryParams, Guid companyId, CancellationToken ct = default);
    Task<PagedList<UserOrderListQueryModel>> GetUserOrdersListAsync(UserOrderListQuery queryParams, Guid userId, CancellationToken ct = default);
}

