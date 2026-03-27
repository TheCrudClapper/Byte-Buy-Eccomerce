using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Filtration.Offer;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.ServiceContracts;

public interface IOfferReadService
{
    Task<Result<PagedList<OfferBrowserItemResponse>>> BrowseAsync(OfferBrowserQuery queryParams, CancellationToken ct);
    Task<Result<RentOfferDetailsResponse>> GetRentOfferDetailsAsync(Guid id, CancellationToken ct = default);
    Task<Result<SaleOfferDetailsResponse>> GetSaleOfferDetailsAsync(Guid id, CancellationToken ct = default);
    Task<Result<PagedList<UserPanelOfferResponse>>> GetUserPanelOffersAsync(UserOffersQuery queryParams, Guid userId, CancellationToken ct = default);
}
