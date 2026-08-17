using ByteBuy.Core.Domain.Users.Entities;

namespace ByteBuy.Infrastructure.Repositories;

public class AddressReadRepository : EfBaseRepository<ShippingAddress>, IAddressReadRepository
{
    public AddressReadRepository(ApplicationDbContext context) : base(context) { }
}
