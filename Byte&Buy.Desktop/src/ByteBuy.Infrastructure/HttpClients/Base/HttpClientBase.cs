using ByteBuy.Services.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ByteBuy.Infrastructure.HttpClients.Base;

public abstract class HttpClientBase
{
    protected readonly HttpClient Client;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IConfiguration _configuration;

    protected HttpClientBase(HttpClient httpClient, IConfiguration configuration)
    {
        Client = httpClient;
        _configuration = configuration;

        Client.BaseAddress = new(_configuration.GetSection("Environment")
            .GetValue<string>("DefaultApiUrl") ?? "http://localhost:5099/api/");

        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    protected async Task<Result> GetAsync(string url)
    {
        try
        {
            var response = await Client.GetAsync(url);
            return await HandleResponseAsync(response);
        }
        catch (Exception)
        {
            return Result.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result<T>> GetAsync<T>(string url)
    {
        try
        {
            var response = await Client.GetAsync(url);
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
            var response = await Client.PostAsync(resource, content);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception)
        {
            return Result<T>.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result> PostAsync(string resource, object payload)
    {
        try
        {
            var serialized = CreateJsonContent(payload);
            var response = await Client.PostAsync(resource, serialized);
            return await HandleResponseAsync(response);
        }
        catch (Exception)
        {
            return Result.Fail(ApiErrors.RequestFailed);
        }
    }

    protected async Task<Result<T>> PostAsync<T>(string resource, object payload)
    {
        try
        {
            var serialized = CreateJsonContent(payload);
            var response = await Client.PostAsync(resource, serialized);
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
            var response = await Client.PutAsync(resource, content);
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
            var serialized = CreateJsonContent(payload);
            var response = await Client.PutAsync(resource, serialized);
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
            var serialized = CreateJsonContent(payload);
            var response = await Client.PutAsync(resource, serialized);
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
            var response = await Client.DeleteAsync(resource);
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

}
