using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class CompanyInfoService(ICompanyInfoHttpClient companyInfoClient) : ICompanyInfoService
{
    public Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync()
        => companyInfoClient.GetCompanyInfoAsync();

    public Task<Result<UpdatedResponse>> UpdateAsync(CompanyInfoUpdateRequest request)
        => companyInfoClient.PutCompanyInfoAsync(request);
}