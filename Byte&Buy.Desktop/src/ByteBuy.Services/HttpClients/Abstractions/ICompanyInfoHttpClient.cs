using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface ICompanyInfoHttpClient
{
    Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync();
    Task<Result<UpdatedResponse>> UpdateCompanyInfoAsync(CompanyInfoUpdateRequest request);
}