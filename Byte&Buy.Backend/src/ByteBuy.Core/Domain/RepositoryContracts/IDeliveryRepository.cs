using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDeliveryRepository : IRepositoryBase<Delivery>
{
    Task<bool> HasActiveRelations(Guid deliveryId);
    Task<bool> ExistWithNameAsync(string name, Guid? exludeId = null);
    Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyCollection<Delivery>> GetAllByIdsAsync(IEnumerable<Guid> ids,  CancellationToken ct = default);
}
