using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICompanyInfoRepository
{
    Task AddAsync(CompanyInfo companyInfo);
    Task<bool> ExistAsync(CancellationToken ct = default);
    Task<CompanyInfo?> GetAsync(CancellationToken ct = default);
    Task UpdateAsync(CompanyInfo companyInfo);
}
