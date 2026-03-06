using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Configuration;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyHttpClient(HttpClient httpClient, IConfiguration config)
    : HttpClientBase(httpClient, config), ICompanyInfoHttpClient
{
    private const string resource = "company/info";
    public Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync()
        => GetAsync<CompanyInfoResponse>($"{resource}");

    public Task<Result<UpdatedResponse>> PutCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => PutAsync<UpdatedResponse>($"{resource}", request);
}