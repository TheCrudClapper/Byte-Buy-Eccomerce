using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Order;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class OrderSpecifications
{
    public sealed class UserOrderListQuerySpec : Specification<Order, UserOrderListQuery>
    {
        public UserOrderListQuerySpec(Guid userId)
        {
            Query.AsNoTracking()
                 .Where(o => o.BuyerId == userId)
                 .Select(OrderMappings.UserOrderListQueryProjection);
        }
    }

    public sealed class OrderDetailsResponseSpec : Specification<Order, OrderDetailsQuery>
    {
        public OrderDetailsResponseSpec(Guid orderId)
        {
            Query
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .Select(OrderMappings.OrderDetailsQueryProjection);
        }
    }

}
