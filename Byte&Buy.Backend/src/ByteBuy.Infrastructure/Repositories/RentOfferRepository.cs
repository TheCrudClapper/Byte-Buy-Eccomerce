using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class RentOfferRepository : EfBaseRepository<RentOffer>, IRentOfferRepository
{
    public RentOfferRepository(ApplicationDbContext context) : base(context) { }
}
