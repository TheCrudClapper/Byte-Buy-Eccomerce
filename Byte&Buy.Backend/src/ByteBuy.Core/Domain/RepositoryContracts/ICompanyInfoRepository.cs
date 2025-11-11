using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICompanyInfoRepository
{
    Task<CompanyInfo> AddAsync(CompanyInfo companyInfo, CancellationToken ct = default);
    Task<CompanyInfo?> GetAsync(Guid companyInfoId, CancellationToken ct = default);
    Task<CompanyInfo> UpdateAsync(CompanyInfo companyInfo, CancellationToken ct = default);
}
