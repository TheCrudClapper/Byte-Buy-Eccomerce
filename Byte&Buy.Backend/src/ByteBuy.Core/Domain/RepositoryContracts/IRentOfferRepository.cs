using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRentOfferRepository : IRepositoryBase<RentOffer>
{
    Task<RentOffer?> GetAggregateAsync(Guid id, CancellationToken ct = default);
}
