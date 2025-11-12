using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICompanyInfoService
{
    Task<Result<CompanyInfoResponse>> AddCompanyInfo(CompanyInfoAddRequest request, CancellationToken ct = default);
    Task<Result<CompanyInfoResponse>> UpdateCompanyInfo(CompanyInfoUpdateRequest request, CancellationToken ct = default);
    Task<Result<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct = default);
}
