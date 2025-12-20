using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDeliveryCarrierRepository : IRepositoryBase<DeliveryCarrier>
{
    Task<bool> HasActiveRelationsAsync(Guid carrierId);
    Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? exludeId = null);
    Task<IEnumerable<DeliveryCarrier>> GetAllAsync(CancellationToken ct = default);
}
