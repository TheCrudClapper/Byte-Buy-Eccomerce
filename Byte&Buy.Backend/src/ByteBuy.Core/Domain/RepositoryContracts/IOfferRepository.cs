using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IOfferRepository : IRepositoryBase<Offer>
{
    Task<IReadOnlyCollection<Offer>> GetOffersByIdsAsync(IEnumerable<Guid> offerIds, CancellationToken ct = default);
    Task<IReadOnlyCollection<Offer>> GetOffersCreatedByUser(Guid userId, CancellationToken ct = default);
}
