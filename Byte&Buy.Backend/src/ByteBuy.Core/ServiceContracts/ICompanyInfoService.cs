using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICompanyInfoService
{
    Task<Result<CompanyInfoResponse>> AddCompanyInfo(CompanyInfoAddRequest request);
    Task<Result<CompanyInfoResponse>> UpdateCompanyInfo(CompanyInfoUpdateRequest request);
    Task<Result<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct = default);
}
