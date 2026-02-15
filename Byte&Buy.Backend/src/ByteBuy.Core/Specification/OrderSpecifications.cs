using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Order;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class OrderSpecifications
{

    /// <summary>
    /// Specification that gets orders for specific seller
    /// </summary>
    public sealed class SellerOrdersListQueryModelSpec : Specification<Order, UserOrderListQueryModel>
    {
        public SellerOrdersListQueryModelSpec(Guid sellerId)
        {
            Query.AsNoTracking()
                 .Where(o => o.SellerSnapshot.SellerId == sellerId)
                 .Select(OrderMappings.UserOrderListQueryModelProjection);
        }
    }

    /// <summary>
    /// Specification that get order details for two ends: user and private seller
    /// </summary>
    public sealed class OrderDetailsQueryModelSpec : Specification<Order, OrderDetailsQueryModel>
    {
        public OrderDetailsQueryModelSpec(Guid userId, Guid orderId)
        {
            Query
                .AsNoTracking()
                .Where(o => o.Id == orderId
                    && (o.BuyerId == userId
                    || (o.SellerSnapshot.SellerId == userId && o.SellerSnapshot.Type == SellerType.PrivatePerson)))
                .Select(OrderMappings.OrderDetailsQueryModelProjection);
        }
    }

    /// <summary>
    /// Specification that gets and order details for company
    /// </summary>
    public sealed class CompanyOrderDetailsQueryModelSpec : Specification<Order, OrderDetailsQueryModel>
    {
        public CompanyOrderDetailsQueryModelSpec(Guid companyId, Guid orderId)
        {
            Query.AsNoTracking()
                .Where(o => o.Id == orderId
                    && (o.SellerSnapshot.SellerId == companyId && o.SellerSnapshot.Type == SellerType.Company))
                .Select(OrderMappings.OrderDetailsQueryModelProjection);
        }
    }

    /// <summary>
    /// Gets x number of orders for dashboard
    /// </summary>
    public sealed class OrderDashboardListResponseSpec : Specification<Order, OrderDashboardListResponse>
    {
        public OrderDashboardListResponseSpec()
        {
            Query.AsNoTracking()
                .Take(10)
                .OrderByDescending(o => o.DateCreated)
                .Select(OrderMappings.OrderDashboardProjection);
        }
    }
}
