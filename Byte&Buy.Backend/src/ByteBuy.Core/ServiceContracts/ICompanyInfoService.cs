using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICompanyInfoService
{
    Task<Result<CreatedResponse>> AddAsync(CompanyInfoAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(CompanyInfoUpdateRequest request);
    Task<Result<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct = default);
}
