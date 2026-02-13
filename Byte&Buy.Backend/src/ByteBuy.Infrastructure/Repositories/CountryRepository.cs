using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using ByteBuy.Services.Filtration;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class CountryRepository : EfBaseRepository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? excludedId = null)
    {
        return await _context.Countries
            .AnyAsync(c => c.Id != excludedId && (c.Name == name || c.Code == code));
    }

    public async Task<IReadOnlyCollection<Country>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Countries
            .ToListAsync(ct);
    }

    public async Task<PagedList<CountryResponse>> GetListAsync(CountryListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Countries
        .AsNoTracking()
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.CountryName))
            query = query.Where(c => EF.Functions.ILike(c.Name, $"%{queryParams.CountryName}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.Code))
            query = query.Where(c => EF.Functions.ILike(c.Code, $"%{queryParams.Code}%"));

        var projection = query.Select(c => c.ToCountryResponse());

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }

    public async Task<bool> HasActiveRelationsAsync(Guid countryId)
    {
        return await _context.Countries
            .AnyAsync(c => c.Id == countryId && c.Addresses.Any());
    }
}
