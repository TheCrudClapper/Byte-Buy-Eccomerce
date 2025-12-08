using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyInfoHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), ICompanyInfoHttpClient
{
    private const string resource = "companyInfo";
    public Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync()
        => GetAsync<CompanyInfoResponse>($"{resource}");

    public Task<Result<UpdatedResponse>> PutCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => PutAsync<UpdatedResponse>($"{resource}", request);
}