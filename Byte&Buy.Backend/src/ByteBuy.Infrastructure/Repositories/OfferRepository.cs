using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Money;
using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.Mappings;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class OfferRepository : EfBaseRepository<Offer>, IOfferRepository
{
    public OfferRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyCollection<OfferBrowserItemResponse>> BrowseOffers(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
