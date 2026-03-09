using ByteBuy.Core.Domain.Companies;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class CompanyRepository : EfBaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistAsync(CancellationToken ct = default)
    {
        return await _context.Company
            .AnyAsync(ct);
    }

    public async Task<Company?> GetAsync(CancellationToken ct)
    {
        //There is only one record of company details in whole db
        return await _context.Company
            .SingleOrDefaultAsync(ct);
    }

    public async Task<Guid> GetCompanyId(CancellationToken ct = default)
    {
        return await _context.Company
            .Select(c => c.Id)
            .SingleOrDefaultAsync(ct);
    }
}
