using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options, ISettingsService settingsService)
    : HttpClientBase(httpClient, options, settingsService), ICompanyInfoHttpClient
{
    private readonly string resource = options.Value.Company;
    public Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync()
        => GetAsync<CompanyInfoResponse>($"{resource}");

    public Task<Result<UpdatedResponse>> PutCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => PutAsync<UpdatedResponse>($"{resource}", request);
}