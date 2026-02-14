using Ardalis.Specification.EntityFrameworkCore;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.Offer;
using ByteBuy.Core.Filtration.Offer;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class OfferRepository : EfBaseRepository<Offer>, IOfferRepository
{
    public OfferRepository(ApplicationDbContext context) : base(context) { }

    public async Task<PagedList<OfferBrowserItemQuery>> BrowserAsync(OfferBrowserQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Offers
            .AsNoTracking()
            .AsQueryable();

        if (queryParams.ConditionIds is not null && queryParams.ConditionIds.Count > 0)
            query = query.Where(o => queryParams.ConditionIds.Contains(o.Item.ConditionId));

        if (queryParams.CategoryIds is not null && queryParams.CategoryIds.Count > 0)
            query = query.Where(o => queryParams.CategoryIds.Contains(o.Item.CategoryId));

        if (queryParams.SellerType is not null)
            query = query.Where(o => o.Seller.Type == queryParams.SellerType);

        if (!string.IsNullOrWhiteSpace(queryParams.SearchPhrase))
            query = query.Where(o => EF.Functions.ILike(o.Item.Name, $"%{queryParams.SearchPhrase}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.City))
            query = query.Where(o => EF.Functions.ILike(o.OfferAddressSnapshot.City, $"%{queryParams.City}%"));


        if (queryParams.MinPrice is not null)
        {
            query = query.Where(o => (((SaleOffer)o).PricePerItem.Amount >= queryParams.MinPrice)
                                  || (((RentOffer)o).PricePerDay.Amount >= queryParams.MinPrice));
        }

        if (queryParams.MaxPrice is not null)
        {
            query = query.Where(o => (((SaleOffer)o).PricePerItem.Amount <= queryParams.MaxPrice)
                                  || (((RentOffer)o).PricePerDay.Amount <= queryParams.MaxPrice));
        }

        if (queryParams.MaxRentalDays is not null)
        {
            query = query.Where(o => ((RentOffer)o).MaxRentalDays <= queryParams.MaxRentalDays);
        }

        if (queryParams.MinRentalDays is not null)
        {
            query = query.Where(o => ((RentOffer)o).MaxRentalDays >= queryParams.MinRentalDays);
        }

        query = queryParams.SortBy switch
        {
            OfferSortBy.Oldest => query.OrderBy(o => o.DateCreated),
            OfferSortBy.Newest => query.OrderByDescending(o => o.DateCreated),
            _ => query,
        };

        query = queryParams.SellerType switch
        {
            SellerType.PrivatePerson => query.Where(o => o.Seller.Type == SellerType.PrivatePerson),
            SellerType.Company => query.Where(o => o.Seller.Type == SellerType.Company),
            _ => query,
        };

        var projection = query.Select(OfferMappings.OfferBrowserItemQueryProjection);

        return await projection
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }

    public async Task<PagedList<UserPanelOfferQuery>> GetUserOffersAsync(UserOffersQuery queryParams, Guid userId, CancellationToken ct = default)
    {
        var query = _context.Offers
             .Where(o => o.CreatedByUserId == userId)
             .AsNoTracking()
             .AsQueryable();

        var projection = query.Select(OfferMappings.UserPanelOfferQueryProjection);

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }

    public async Task<IReadOnlyCollection<Offer>> GetOffersByIdsAsync(IEnumerable<Guid> offerIds, CancellationToken ct = default)
    {
        return await _context.Offers
            .Where(o => offerIds.Contains(o.Id))
            .ToListAsync(ct);
    }


    public async Task<IReadOnlyCollection<Offer>> GetOffersCreatedByUser(Guid userId, CancellationToken ct = default)
    {
        return await _context.Offers
            .Include(o => o.OfferDeliveries)
            .Where(o => o.CreatedByUserId == userId)
            .ToListAsync(ct);
    }

}
