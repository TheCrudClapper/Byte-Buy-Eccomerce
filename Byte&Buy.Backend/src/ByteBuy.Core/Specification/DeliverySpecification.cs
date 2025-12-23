using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class DeliverySpecifications
{
    public class DeliveryToDeliveryListResponseSpec : Specification<Delivery, DeliveryListResponse>
    {
        public DeliveryToDeliveryListResponseSpec()
        {
            Query.Select(DeliveryMappings.DeliveryListResponseProjection);
        }
    }

    public class DeliveryToSelectListItemSpec : Specification<Delivery, SelectListItemResponse<Guid>>
    {
        public DeliveryToSelectListItemSpec()
        {
            Query.Select(DeliveryMappings.DeliverySelectListProjection);
        }
    }

    public class DeliveryToDeliveryOptionResponseSpec : Specification<Delivery, DeliveryOptionResponse>
    {
        public DeliveryToDeliveryOptionResponseSpec()
        {
            Query.Select(DeliveryMappings.DeliveryOptionResponseProjection);
        }
    }
}
