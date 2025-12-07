using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IAddressRepository : IRepositoryBase<Address>
{
    Task<bool> DoesAddressWithLabelExists(Guid userId, string label);
    Task<Address?> GetByIdAsync(Guid addressId, CancellationToken ct = default);
}
