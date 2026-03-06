using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanyInfoHttpClient
{
    private const string resource = "company/info";
    public Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync()
        => GetAsync<CompanyInfoResponse>($"{resource}");

    public Task<Result<UpdatedResponse>> PutCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => PutAsync<UpdatedResponse>($"{resource}", request);
}