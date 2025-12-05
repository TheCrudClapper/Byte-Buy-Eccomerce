using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;
public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(Guid addressId, CancellationToken ct = default);
}
