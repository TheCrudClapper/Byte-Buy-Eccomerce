using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Order;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class OrderSpecifications
{
    /// <summary>
    /// Specification that gets orders for two ends: user and private seller
    /// </summary>
    public sealed class UserOrderListQuerySpec : Specification<Order, UserOrderListQuery>
    {
        public UserOrderListQuerySpec(Guid userId)
        {
            Query.AsNoTracking()
                 .Where(o => o.BuyerId == userId 
                    || (o.SellerSnapshot.SellerId == userId && o.SellerSnapshot.Type == SellerType.PrivatePerson))
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

}
