using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Services.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ByteBuy.Infrastructure.HttpClients.Base;

public abstract class HttpClientBase
{
    protected readonly IBackendHttpClientFactory _clientFactory;
    protected bool RequiresAuthentication { get; set; }
    private readonly JsonSerializerOptions _jsonOptions;

    protected HttpClientBase(IBackendHttpClientFactory clientFactory, bool requireAuth = true)
    {
        _clientFactory = clientFactory;
        RequiresAuthentication = requireAuth;
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    protected async Task<Result> GetAsync(string resource)
    {
        try
        {
            using var client = CreateClient();
            using var response = await client.GetAsync(resource);
            return await HandleResponseAsync(response);
        }
        catch (Exception)
        {
            return Result.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result<T>> GetAsync<T>(string resource)
    {
        try
        {
            using var client = CreateClient();
            using var response = await client.GetAsync(resource);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception)
        {
            return Result<T>.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result<T>> PostAsync<T>(string resource, HttpContent content)
    {
        try
        {
            using var client = CreateClient();
            using var response = await client.PostAsync(resource, content);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception)
        {
            return Result<T>.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result<T>> PostAsync<T>(string resource, object payload)
    {
        try
        {
            using var client = CreateClient();
            var serialized = CreateJsonContent(payload);
            using var response = await client.PostAsync(resource, serialized);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception)
        {
            return Result<T>.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result<T>> PutAsync<T>(string resource, HttpContent? content)
    {
        try
        {
            using var client = CreateClient();
            using var response = await client.PutAsync(resource, content);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception)
        {
            return Result<T>.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result> PutAsync(string resource, object payload)
    {
        try
        {
            using var client = CreateClient();
            var serialized = CreateJsonContent(payload);
            using var response = await client.PutAsync(resource, serialized);
            return await HandleResponseAsync(response);
        }
        catch (Exception)
        {
            return Result.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result<T>> PutAsync<T>(string resource, object payload)
    {
        try
        {
            using var client = CreateClient();
            var serialized = CreateJsonContent(payload);
            using var response = await client.PutAsync(resource, serialized);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception)
        {
            return Result<T>.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result> DeleteAsync(string resource)
    {
        try
        {
            using var client = CreateClient();
            using var response = await client.DeleteAsync(resource);
            return await HandleResponseAsync(response);
        }
        catch (Exception)
        {
            return Result.Fail(ApiErrors.RequestFailed);
        }
    }

    private async Task<Result<T>> HandleResponseAsync<T>(HttpResponseMessage response)
    {

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var payload = await response.Content.ReadFromJsonAsync<T>(_jsonOptions);

                if (payload is null)
                    return Result<T>.Fail(ApiErrors.FetchedResourceIsNull);

                return Result<T>.Ok(payload);
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(new Error($"Invalid JSON: {ex.Message}"));
            }
        }

        var error = await ExtractErrorAsync(response);
        return Result<T>.Fail(error);
    }

    private async Task<Result> HandleResponseAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return Result.Ok();

        Error error = await ExtractErrorAsync(response);
        return Result.Fail(error);
    }

    private StringContent CreateJsonContent(object payload)
    {
        var json = JsonSerializer.Serialize(payload, _jsonOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task<Error> ExtractErrorAsync(HttpResponseMessage response)
    {
        try
        {
            if (response.StatusCode == HttpStatusCode.Forbidden)
                return new Error("Access to this resource is forbidden");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var body = await response.Content.ReadAsStringAsync();
                return new Error($"Wrong credentials, try again.");
            }

            var details = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (details is null)
                return ApiErrors.UnknownError;

            return new Error(details.Detail);
        }
        catch
        {
            return ApiErrors.UnknownError;
        }
    }

    protected HttpClient CreateClient()
        => _clientFactory.CreateClient(RequiresAuthentication);
}
