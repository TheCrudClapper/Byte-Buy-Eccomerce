using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Item;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using ByteBuy.Services.Filtration;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class ItemRepository : EfBaseRepository<Item>, IItemRepository
{
    public ItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Item?> GetAggregateAsync(Guid itemId, CancellationToken ct = default)
    {
        return await _context.Items
           .Include(i => i.Images)
           .FirstOrDefaultAsync(i => i.Id == itemId, ct);
    }

    public async Task<PagedList<ItemListResponse>> GetCompanyItemsAsync(ItemListQuery queryParam, CancellationToken ct = default)
    {
        var query = _context.Items
            .AsNoTracking()
            .AsQueryable();

        if(!string.IsNullOrWhiteSpace(queryParam.Name))
            query = query.Where(i => i.Name.Contains(queryParam.Name));

        if(queryParam.StockQuantityFrom.HasValue)
            query = query.Where(i => i.StockQuantity >= queryParam.StockQuantityFrom.Value);

        if (queryParam.StockQuantityTo.HasValue)
            query = query.Where(i => i.StockQuantity <= queryParam.StockQuantityTo.Value);

        var projection = query.Select(ItemsMappings.ItemListResponseProjection);

        return await projection.ToPagedListAsync(queryParam.PageNumber, queryParam.PageSize);
    }

    public async Task<bool> HasActiveRelationsAsync(Guid itemId)
        => await _context.Items
        .Where(i => i.Id == itemId)
        .AnyAsync(i => i.Offers.Any());

}
