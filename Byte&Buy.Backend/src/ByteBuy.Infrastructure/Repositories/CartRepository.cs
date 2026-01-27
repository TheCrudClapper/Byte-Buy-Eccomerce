using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;

namespace ByteBuy.Infrastructure.Repositories;

public class CartRepository : EfBaseRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }
}
