using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ICompanyInfoHttpClient
{
    Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync();
    Task<Result<UpdatedResponse>> UpdateCompanyInfoAsync(CompanyInfoUpdateRequest request);
}