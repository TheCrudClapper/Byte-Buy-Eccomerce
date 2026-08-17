using ByteBuy.Core.Domain.Companies;

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
