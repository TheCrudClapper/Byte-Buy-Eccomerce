using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICompanyInfoService
{
    Task<Result<CreatedResponse>> AddCompanyInfo(CompanyInfoAddRequest request);
    Task<Result<UpdatedResponse>> UpdateCompanyInfo(CompanyInfoUpdateRequest request);
    Task<Result<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct = default);
}
