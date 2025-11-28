using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface ICompanyInfoHttpClient
{
    Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync();
    Task<Result<CompanyInfoResponse>> UpdateCompanyInfoAsync(CompanyInfoUpdateRequest request);
}