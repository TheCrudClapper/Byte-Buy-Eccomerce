using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Filtration.Offer;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IOfferReadService
{
    Task<Result<PagedList<OfferBrowserItemResponse>>> BrowseAsync(OfferBrowserQuery queryParams, CancellationToken ct);
    Task<Result<RentOfferDetailsResponse>> GetRentOfferDetails(Guid id, CancellationToken ct = default);
    Task<Result<SaleOfferDetailsResponse>> GetSaleOfferDetails(Guid id, CancellationToken ct = default);
    Task<Result<PagedList<UserPanelOfferResponse>>> GetUserPanelOffers(UserOffersQuery queryParams, Guid userId, CancellationToken ct = default);
}
