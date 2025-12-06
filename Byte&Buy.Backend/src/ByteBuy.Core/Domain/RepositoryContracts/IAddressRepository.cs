using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;
public interface IAddressRepository
{
    Task AddAsync(Address address);
    Task UpdateAsync(Address address);
    Task<bool> DoesUserHaveAdresses(Guid userId);
    Task<Address?> GetCurrentDefault(Guid userId);
    Task<Address?> GetByIdAsync(Guid addressId, CancellationToken ct = default);
    Task<Address?> GetUserAddressAsync(Guid addressId, Guid userId, CancellationToken ct = default);
    Task<bool> DoesAddressWithLabelExists(Guid userId, string label);
    Task<IEnumerable<Address>> GetUserAdressesAsync(Guid userId, CancellationToken ct = default);
    Task<int> CommitAsync();
}
