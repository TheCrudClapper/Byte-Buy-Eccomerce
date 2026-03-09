using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.Users.Entities;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;

namespace ByteBuy.Infrastructure.Repositories;

public class AddressReadRepository : EfBaseRepository<ShippingAddress>, IAddressReadRepository
{
    public AddressReadRepository(ApplicationDbContext context) : base(context) { }
}
