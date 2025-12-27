using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class CompanyInfoRepository : EfBaseRepository<CompanyInfo>, ICompanyInfoRepository
{
    public CompanyInfoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistAsync(CancellationToken ct = default)
    {
        return await _context.CompanyInfo.AnyAsync(ct);
    }

    public async Task<CompanyInfo?> GetAsync(CancellationToken ct)
    {
        //There is only one record of company details in whole db
        return await _context.CompanyInfo
            .FirstOrDefaultAsync();
    }
}
