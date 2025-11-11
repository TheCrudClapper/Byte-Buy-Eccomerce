using ByteBuy.Core.DTO.CompanyInfo;

namespace ByteBuy.Core.ServiceContracts;

public interface ICompanyInfoService
{
    Task<CompanyInfoResponse> AddCompanyInfo(CompanyInfoAddRequest request, CancellationToken cancellationToken = default);
    Task<CompanyInfoResponse> UpdateCompanyInfo(Guid companyInfoId,  CompanyInfoAddRequest request, CancellationToken cancellationToken = default);
    Task<CompanyInfoResponse> GetCompanyInfo(Guid companyInfoId, CancellationToken cancellationToken = default);
}
