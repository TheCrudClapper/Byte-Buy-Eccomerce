using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyUserHomeAddressHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanyUserHomeAddressHttpClient
{
    private readonly string resource = options.Value.Users;

    public async Task<Result<UpdatedResponse>> PutUserHomeAddressAsync(Guid userId, HomeAddressDto request)
        => await PutAsync<UpdatedResponse>($"{resource}/{userId}/home-address", request);
}
