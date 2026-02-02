using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Order;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class OrderSpecifications
{
    public sealed class UserOrderListQuerySpec : Specification<Order, UserOrderListQuery>
    {
        public UserOrderListQuerySpec(Guid userId)
        {
            Query.Where(o => o.BuyerId == userId)
                 .Select(OrderMappings.UserOrderListQueryProjection);
        }
    }
}
