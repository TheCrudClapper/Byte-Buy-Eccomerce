using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class CompanyInfoService(ICompanyInfoHttpClient companyInfoClient) : ICompanyInfoService
{
    public Task<Result<CompanyInfoResponse>> GetCompanyInfo()
        => companyInfoClient.GetCompanyInfoAsync();

    public Task<Result<UpdatedResponse>> UpdateCompanyInfo(CompanyInfoUpdateRequest request)
        => companyInfoClient.UpdateCompanyInfoAsync(request);
}