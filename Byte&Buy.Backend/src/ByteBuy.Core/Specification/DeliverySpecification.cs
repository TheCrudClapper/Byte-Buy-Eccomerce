using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class DeliverySpecifications
{
    public class DeliveryToDeliveryListResponseSpec : Specification<Delivery, DeliveryListResponse>
    {
        public DeliveryToDeliveryListResponseSpec(Guid? offerId = null)
        {
            if (offerId.HasValue)
                Query.Where(d => d.OfferDeliveries.Any(od => od.OfferId == offerId && od.IsActive));

            Query.AsNoTracking()
                .Select(DeliveryMappings.DeliveryListResponseProjection);
        }
    }

    public class DeliveryToSelectListItemSpec : Specification<Delivery, SelectListItemResponse<Guid>>
    {
        public DeliveryToSelectListItemSpec()
        {
            Query.AsNoTracking()
                .Select(DeliveryMappings.DeliverySelectListProjection);
        }
    }

    public class DeliveryToDeliveryOptionResponseSpec : Specification<Delivery, DeliveryOptionResponse>
    {
        public DeliveryToDeliveryOptionResponseSpec()
        {
            Query.AsNoTracking()
                .Select(DeliveryMappings.DeliveryOptionResponseProjection);
        }
    }
}
