using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class CompanyInfoRepository : BaseRepository, ICompanyInfoRepository
{
    public CompanyInfoRepository(ApplicationDbContext context) : base(context){}

    public async Task<CompanyInfo> AddAsync(CompanyInfo companyInfo, CancellationToken ct)
    {
        await _context.CompanyInfo.AddAsync(companyInfo, ct);
        await _context.SaveChangesAsync(ct);
        return companyInfo;
    }

    public async Task<CompanyInfo?> GetAsync(Guid companyInfoId, CancellationToken ct)
    {
        return await _context.CompanyInfo
            .FirstOrDefaultAsync(ci => ci.Id == companyInfoId, ct);
    }

    public async Task<CompanyInfo> UpdateAsync(CompanyInfo companyInfo, CancellationToken ct)
    {
        _context.CompanyInfo.Update(companyInfo);
        await _context.SaveChangesAsync(ct);
        return companyInfo;
    }
}
