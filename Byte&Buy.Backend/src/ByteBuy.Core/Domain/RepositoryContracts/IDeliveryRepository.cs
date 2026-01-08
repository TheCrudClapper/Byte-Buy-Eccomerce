using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDeliveryRepository : IRepositoryBase<Delivery>
{
    Task<bool> HasActiveRelations(Guid deliveryId);
    Task<bool> ExistWithNameAsync(string name, Guid? exludeId = null);
    Task<IReadOnlyCollection<Delivery>> GetAllByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
    Task<List<decimal>> GetCheapestCostByOfferIds(IEnumerable<Guid> offerIds);
}
