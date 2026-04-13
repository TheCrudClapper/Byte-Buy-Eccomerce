using ByteBuy.Core.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Extensions;

public static class IQueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        var totalCount = await source.CountAsync(ct);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        if (totalCount == 0)
            pageNumber = 1;
        else if(pageNumber > totalPages)
            pageNumber = totalPages;

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        var metadata = new PaginationMetadata
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNext = pageNumber < totalPages,
            HasPrevious = pageNumber > 1,
        };

        return new PagedList<T>
        {
            Items = items,
            Metadata = metadata
        };
    }
}
