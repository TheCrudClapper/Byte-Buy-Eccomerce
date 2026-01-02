using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;

namespace ByteBuy.Infrastructure.Repositories;

public class AddressRepository : EfBaseRepository<ShippingAddress>, IAddressReadRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context) { }
}
