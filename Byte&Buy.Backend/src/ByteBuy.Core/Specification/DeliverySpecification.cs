using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Delivery;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class DeliverySpecifications
{
    public class DeliveryListResponseSpec : Specification<Delivery, DeliveryListResponse>
    {
        public DeliveryListResponseSpec(Guid? offerId = null)
        {
            if (offerId.HasValue)
                Query.Where(d => d.OfferDeliveries.Any(od => od.OfferId == offerId && od.IsActive));

            Query.AsNoTracking()
                .OrderBy(d => d.Price.Amount)
                .Select(DeliveryMappings.DeliveryListResponseProjection);
        }
    }

    public class DeliverySelectListItemSpec : Specification<Delivery, SelectListItemResponse<Guid>>
    {
        public DeliverySelectListItemSpec()
        {
            Query.AsNoTracking()
                .Select(DeliveryMappings.DeliverySelectListProjection);
        }
    }

    public class DeliveryOptionResponseSpec : Specification<Delivery, DeliveryOptionResponse>
    {
        public DeliveryOptionResponseSpec()
        {
            Query.AsNoTracking()
                .Select(DeliveryMappings.DeliveryOptionResponseProjection);
        }
    }

    public class DeliveryOptionByIdsSpec : Specification<Delivery, DeliveryOptionResponse>
    {
        public DeliveryOptionByIdsSpec(IEnumerable<Guid> deliveryIds)
        {
            Query.AsNoTracking()
                .Where(d => deliveryIds.Contains(d.Id))
                .Select(DeliveryMappings.DeliveryOptionResponseProjection);
        }
    }

    public class DeliveryOrderQuerySpec : Specification<Delivery, DeliveryOrderQueryModel>
    {
        public DeliveryOrderQuerySpec(IEnumerable<Guid> deliveryIds)
        {
            Query.AsNoTracking()
                 .Where(d => deliveryIds.Contains(d.Id))
                 .Select(DeliveryMappings.DeliveryOrderQueryModelProjection);
        }
    }
}
