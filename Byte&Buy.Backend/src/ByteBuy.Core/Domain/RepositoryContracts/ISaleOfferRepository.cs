using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ISaleOfferRepository : IRepositoryBase<SaleOffer>
{
    Task<SaleOffer?> GetAggregateAsync(Guid id, CancellationToken ct = default);
}
