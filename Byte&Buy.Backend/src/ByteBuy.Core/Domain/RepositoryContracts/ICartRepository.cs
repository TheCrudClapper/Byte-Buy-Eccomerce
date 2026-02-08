using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Internal.Cart;
using ByteBuy.Core.DTO.Internal.Checkout;
namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICartRepository : IRepositoryBase<Cart>
{
    Task<IReadOnlyCollection<FlatCartOffersQuery>> GetCartOffersForCheckout(Guid userId, CancellationToken ct = default);
    Task<IReadOnlyCollection<CheckoutItemQuery>> GetCartOffersAsCheckoutItemQuery(Guid userId, CancellationToken ct = default);
}
