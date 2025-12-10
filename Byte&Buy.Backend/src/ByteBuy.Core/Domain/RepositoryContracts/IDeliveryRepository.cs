using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDeliveryRepository : IRepositoryBase<Delivery>
{
    Task<bool> HasActiveRelations(Guid deliveryId);
    Task<Delivery?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistWithNameAsync(string name, Guid? exludeId = null);
    Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken ct = default);
}
