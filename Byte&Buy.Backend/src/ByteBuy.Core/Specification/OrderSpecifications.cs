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
    /// Specification that gets orders for specific user
    /// </summary>
    public sealed class UserOrderListQuerySpec : Specification<Order, UserOrderListQuery>
    {
        public UserOrderListQuerySpec(Guid userId)
        {
            Query.AsNoTracking()
                 .OrderByDescending(o => o.Status == OrderStatus.AwaitingPayment)
                 .Where(o => o.BuyerId == userId)
                 .Select(OrderMappings.UserOrderListQueryProjection);
        }
    }

    /// <summary>
    /// Specification that gets orders for specific seller
    /// </summary>
    public sealed class SellerOrdersListQuerySpec : Specification<Order, UserOrderListQuery>
    {
        public SellerOrdersListQuerySpec(Guid sellerId)
        {
            Query.AsNoTracking()
                 .Where(o => o.SellerSnapshot.SellerId == sellerId)
                 .Select(OrderMappings.UserOrderListQueryProjection);
        }
    }

    /// <summary>
    /// Specification that get order details for two ends: user and private seller
    /// </summary>
    public sealed class OrderDetailsResponseSpec : Specification<Order, OrderDetailsQuery>
    {
        public OrderDetailsResponseSpec(Guid userId, Guid orderId)
        {
            Query
                .AsNoTracking()
                .Where(o => o.Id == orderId
                    && (o.BuyerId == userId
                    || (o.SellerSnapshot.SellerId == userId && o.SellerSnapshot.Type == SellerType.PrivatePerson)))
                .Select(OrderMappings.OrderDetailsQueryProjection);
        }
    }

    public sealed class CompanyOrderDetailsResponseSpec : Specification<Order, OrderDetailsQuery>
    {
        public CompanyOrderDetailsResponseSpec(Guid companyId, Guid orderId)
        {
            Query.AsNoTracking()
                .Where(o => o.Id == orderId
                    && (o.SellerSnapshot.SellerId == companyId && o.SellerSnapshot.Type == SellerType.Company))
                .Select(OrderMappings.OrderDetailsQueryProjection);
        }
    }


    /// <summary>
    /// Gets x number of orders for dashboard
    /// </summary>
    public sealed class DashboardOrdersSpec : Specification<Order, OrderDashboardListResponse>
    {
        public DashboardOrdersSpec()
        {
            Query.AsNoTracking()
                .Take(10)
                .OrderByDescending(o => o.DateCreated)
                .Select(OrderMappings.OrderDashboardProjection);
        }
    }
}
