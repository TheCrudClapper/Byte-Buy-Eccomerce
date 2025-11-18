using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ByteBuy.Services.DTO;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.Services.HttpClients.Implementations;

public class AuthHttpClient : IAuthHttpClient
{
    private readonly HttpClient _httpClient;

    public AuthHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/Auth/Login", payload);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return Result<TokenResponse>.Ok(data!);
        }

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return Result<TokenResponse>.Fail(problem);
    }
}