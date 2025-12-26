using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.RentOffer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RentOfferSpecifications
{
    public sealed class RentOfferToRentOfferListResponseSpec : Specification<RentOffer, RentOfferListResponse>
    {
        public RentOfferToRentOfferListResponseSpec()
        {
            Query.AsNoTracking()
                 .Select(RentOfferMappings.RentOfferListProjection);
        }
    }

    public sealed class RentOfferToRentOfferResponseSpec : Specification<RentOffer, RentOfferResponse>
    {
        public RentOfferToRentOfferResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(ro => ro.Id == id)
                 .Select(RentOfferMappings.RentOfferResponseProjection);
        }
    }
}
