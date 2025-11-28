using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class CompanyInfoHttpClient : HttpClientBase, ICompanyInfoHttpClient
{
    public CompanyInfoHttpClient(HttpClient httpClient) : base(httpClient) { }

    public Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync()
        => GetAsync<CompanyInfoResponse>("companyInfo");

    public Task<Result<CompanyInfoResponse>> UpdateCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => PutAsync<CompanyInfoResponse>("companyInfo", request);
}