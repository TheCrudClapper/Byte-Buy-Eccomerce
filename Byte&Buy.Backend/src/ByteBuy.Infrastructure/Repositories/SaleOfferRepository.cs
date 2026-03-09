using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Filtration.SaleOffer;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class SaleOfferRepository : EfBaseRepository<SaleOffer>, ISaleOfferRepository
{
    public SaleOfferRepository(ApplicationDbContext context) : base(context) { }

    public async Task<SaleOffer?> GetAggregateAsync(Guid id, CancellationToken ct = default)
        => await _context.SaleOffers
            .IgnoreQueryFilters()
            .Include(so => so.OfferDeliveries)
            .FirstOrDefaultAsync(so => so.Id == id, ct);

    public async Task<PagedList<SaleOfferListResponse>> GetSaleOffersListAsync(SaleOfferListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.SaleOffers
           .AsNoTracking()
           .Where(so => so.Item.IsCompanyItem)
           .OrderByDescending(so => so.DateCreated)
           .AsQueryable();


        if (!string.IsNullOrWhiteSpace(queryParams.Name))
            query = query.Where(so => EF.Functions.ILike(so.Item.Name, $"%{queryParams.Name}%"));

        if (queryParams.PriceFrom.HasValue)
            query = query.Where(so => so.PricePerItem.Amount >= queryParams.PriceFrom.Value);

        if (queryParams.PriceTo.HasValue)
            query = query.Where(so => so.PricePerItem.Amount <= queryParams.PriceTo.Value);

        if (queryParams.QuantityFrom.HasValue)
            query = query.Where(so => so.QuantityAvailable >= queryParams.QuantityFrom.Value);

        if (queryParams.QuantityTo.HasValue)
            query = query.Where(so => so.QuantityAvailable <= queryParams.QuantityTo.Value);

        var projection = query.Select(SaleOfferMappings.SaleOfferListProjection);

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }
}
