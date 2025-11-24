using ByteBuy.Services.DTO;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using System.Collections.Generic;

namespace ByteBuy.Services.HttpClients.Implementations;

public class PermissionHttpClient(HttpClient httpClient) 
    : HttpClientBase(httpClient), IPermissionHttpClient
{
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>("permissions/options");
}