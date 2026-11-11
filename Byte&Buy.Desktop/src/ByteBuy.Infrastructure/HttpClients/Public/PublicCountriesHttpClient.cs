using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Public;

public class PublicCountriesHttpClient(IBackendHttpClientFactory factory, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(factory, requireAuth: false), IPublicCountriesHttpClient
{
    private readonly string resource = options.Value.CountriesOptions;

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<Guid>>>($"{resource}");
}
