using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOfferReadService
{
    Task<Result<IReadOnlyCollection<OfferBrowserItemResponse>>> BrowseAsync(CancellationToken ct = default);
    Task<Result<RentOfferDetailsResponse>> GetRentOfferDetails(Guid id, CancellationToken ct = default);
    Task<Result<SaleOfferDetailsResponse>> GetSaleOfferDetails(Guid id, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<UserPanelOfferResponse>>> GetUserPanelOffers(Guid userId, CancellationToken ct = default);
}
