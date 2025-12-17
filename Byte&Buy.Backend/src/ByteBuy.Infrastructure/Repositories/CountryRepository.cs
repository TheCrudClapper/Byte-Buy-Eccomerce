using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
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

    public async Task<IEnumerable<Country>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Countries
            .ToListAsync(ct);
    }
    public async Task<Country?> GetByIdAsync(Guid countryId, CancellationToken ct = default)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(c => c.Id == countryId, ct);
    }

    public async Task<bool> HasActiveRelationsAsync(Guid countryId)
    {
        return await _context.Countries
            .AnyAsync(c => c.Id == countryId && c.Addresses.Any());
    }
}
