using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext _context;
    public CountryRepository(ApplicationDbContext context)
    {
       _context = context;
    }

    public async Task AddAsync(Country country)
    {
        await _context.Countries.AddAsync(country);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Country country)
    {
        _context.Countries.Update(country);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Country country)
    {
        _context.Countries.Update(country);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Country>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Countries
            .ToListAsync(ct);
    }
     
    public async Task<IEnumerable<Country>> GetAllByCondition(Expression<Func<Country, bool>> expression, CancellationToken ct = default)
    {
        return await _context.Countries
            .Where(expression)
            .ToListAsync(ct);
    }

    public async Task<Country?> GetByConditionAsync(Expression<Func<Country, bool>> expression, CancellationToken ct = default)
    {
        return await _context.Countries
            .Where(expression)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<Country?> GetByIdAsync(Guid countryId, CancellationToken ct = default)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(c => c.Id == countryId, ct);
    }

    
}
