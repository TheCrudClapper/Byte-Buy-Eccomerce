using ByteBuy.Core.DTO.Offer;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class OfferReadService : IOfferReadService
{
    public Task<Result<IReadOnlyCollection<OfferListResponse>>> BrowseAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<RentOfferDetailsResponse>> GetRentOfferDetails(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SaleOfferDetailsResponse>> GetSaleOfferDetails(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
