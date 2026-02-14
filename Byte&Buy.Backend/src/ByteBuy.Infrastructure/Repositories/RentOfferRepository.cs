using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.Filtration.RentOffer;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class RentOfferRepository : EfBaseRepository<RentOffer>, IRentOfferRepository
{
    public RentOfferRepository(ApplicationDbContext context) : base(context) { }

    public async Task<PagedList<RentOfferListResponse>> GetRentOffersListAsync(RentOfferListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.RentOffers
            .AsNoTracking()
            .AsQueryable();


        if (!string.IsNullOrWhiteSpace(queryParams.Name))
            query = query.Where(r => EF.Functions.ILike(r.Item.Name, $"%{queryParams.Name}%"));

        if (queryParams.PriceFrom.HasValue)
            query = query.Where(r => r.PricePerDay.Amount >= queryParams.PriceFrom.Value);

        if (queryParams.PriceTo.HasValue)
            query = query.Where(r => r.PricePerDay.Amount <= queryParams.PriceTo.Value);

        if (queryParams.MaxRentalDaysFrom.HasValue)
            query = query.Where(r => r.MaxRentalDays >= queryParams.MaxRentalDaysFrom.Value);

        if (queryParams.MaxRentalDaysTo.HasValue)
            query = query.Where(r => r.MaxRentalDays <= queryParams.MaxRentalDaysTo.Value);

        if (queryParams.QuantityFrom.HasValue)
            query = query.Where(r => r.QuantityAvailable >= queryParams.QuantityFrom.Value);

        if (queryParams.QuantityTo.HasValue)
            query = query.Where(r => r.QuantityAvailable <= queryParams.QuantityTo.Value);

        var projection = query.Select(RentOfferMappings.RentOfferListResponseProjection);

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }
}
