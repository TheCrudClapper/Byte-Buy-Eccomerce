using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class CompanyInfoRepository : BaseRepository, ICompanyInfoRepository
{
    public CompanyInfoRepository(ApplicationDbContext context) : base(context){}

    public async Task AddAsync(CompanyInfo companyInfo, CancellationToken ct)
    {
        await _context.CompanyInfo.AddAsync(companyInfo, ct);
        await _context.SaveChangesAsync(ct);
    }

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

    public async Task UpdateAsync(CompanyInfo companyInfo, CancellationToken ct)
    {
        _context.CompanyInfo.Update(companyInfo);
        await _context.SaveChangesAsync(ct);
    }
}
