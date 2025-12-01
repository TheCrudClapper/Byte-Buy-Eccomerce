using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.CompanyInfo;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class CompanyInfoHttpClient(HttpClient httpClient) 
    : HttpClientBase(httpClient), ICompanyInfoHttpClient
{
    public Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync()
        => GetAsync<CompanyInfoResponse>("companyInfo");

    public Task<Result<UpdatedResponse>> UpdateCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => PutAsync<UpdatedResponse>("companyInfo", request);
}