using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICompanyInfoService
{
    Task<Result<CompanyInfoResponse>> GetCompanyInfo();
    Task<Result<UpdatedResponse>> Update(CompanyInfoUpdateRequest request);
}