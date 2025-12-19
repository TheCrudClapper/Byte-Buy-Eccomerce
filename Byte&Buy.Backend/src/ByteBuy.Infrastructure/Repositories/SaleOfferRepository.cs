using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;

namespace ByteBuy.Infrastructure.Repositories;

public class SaleOfferRepository : EfBaseRepository<SaleOffer>, ISaleOfferRepository
{
    public SaleOfferRepository(ApplicationDbContext context) : base(context) { }
}
