using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Internal.Offer;
using ByteBuy.Core.Filtration.Offer;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IOfferRepository : IRepositoryBase<Offer>
{
    Task<IReadOnlyCollection<Offer>> GetOffersByIdsAsync(IEnumerable<Guid> offerIds, CancellationToken ct = default);
    Task<IReadOnlyCollection<Offer>> GetOffersCreatedByUser(Guid userId, CancellationToken ct = default);
    Task<PagedList<OfferBrowserItemQuery>> BrowserAsync(OfferBrowserQuery queryParams, CancellationToken ct = default);
    Task<PagedList<UserPanelOfferQuery>> GetUserOffersAsync(UserOffersQuery queryParams, Guid userId, CancellationToken ct = default);
}
