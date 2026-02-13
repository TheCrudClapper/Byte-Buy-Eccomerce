using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<IReadOnlyCollection<Order>> GetOrdersByPaymentId(Guid userId, Guid paymentId, CancellationToken ct = default);
    Task<Order?> GetUserOrder(Guid userId, Guid orderId, CancellationToken ct = default);
    Task<Order?> GetSellerOrder(Guid sellerId, Guid orderId, CancellationToken ct = default);
    Task<PagedList<CompanyOrderListResponse>> GetOrdersList(OrderCompanyListQuery queryParams, Guid companyId, CancellationToken ct = default);
}

