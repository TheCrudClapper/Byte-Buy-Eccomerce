using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICartRepository
{
    Task AddCart(Cart cart);
}
