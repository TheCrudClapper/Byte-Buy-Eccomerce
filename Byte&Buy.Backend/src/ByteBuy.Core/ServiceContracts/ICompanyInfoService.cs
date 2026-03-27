using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.CompanyInfo;
using ByteBuy.Core.DTO.Public.Shared;

namespace ByteBuy.Core.ServiceContracts;

public interface ICompanyInfoService
{
    Task<Result<CreatedResponse>> AddAsync(CompanyInfoAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(CompanyInfoUpdateRequest request);
    Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync(CancellationToken ct = default);
}
