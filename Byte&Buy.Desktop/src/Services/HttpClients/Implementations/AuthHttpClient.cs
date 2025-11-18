using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ByteBuy.Services.DTO;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.Services.HttpClients.Implementations;

public class AuthHttpClient(HttpClient httpClient) : IAuthHttpClient
{
    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
    {
        var payload = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        HttpResponseMessage response;
        try
        {
            response = await httpClient.PostAsync("/api/Auth/Login", payload);
        }
        catch (Exception)
        {
            return Result<TokenResponse>.Fail(Error.ConectivityError);
        }
        
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return data is null ? Result<TokenResponse>.Fail(ApiErrors.FetchedResourceIsNull) : Result<TokenResponse>.Ok(data);
        }

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return problem is null ? Result<TokenResponse>.Fail(ApiErrors.UnknownError) : Result<TokenResponse>.Fail(new Error(problem!.Detail));
        
    }
}