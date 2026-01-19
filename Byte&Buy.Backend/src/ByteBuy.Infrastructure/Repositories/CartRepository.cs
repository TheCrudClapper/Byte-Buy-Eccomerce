using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Cart;
using ByteBuy.Core.DTO.Cart.CartItem;
using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;
using ByteBuy.Core.Mappings;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;

namespace ByteBuy.Infrastructure.Repositories;

public class CartRepository : EfBaseRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }
}
