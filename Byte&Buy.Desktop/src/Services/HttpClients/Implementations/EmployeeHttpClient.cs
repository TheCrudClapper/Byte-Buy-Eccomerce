using System.Net.Http.Json;
using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.Services.HttpClients.Implementations;

public class EmployeeHttpClient(HttpClient httpClient) : IEmployeeHttpClient
{
    public async Task<Result<EmployeeResponse>> GetSelfAsync()
    {
        HttpResponseMessage response; 
        try
        {
            response = await httpClient.GetAsync("/api/employees/me");
        }
        catch (Exception)
        {
            return Result<EmployeeResponse>.Fail(Error.ConectivityError);
        }
        
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<EmployeeResponse>();
            return data is null 
                ? Result<EmployeeResponse>.Fail(ApiErrors.FetchedResourceIsNull) 
                : Result<EmployeeResponse>.Ok(data);
        }

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        return problem is null 
            ? Result<EmployeeResponse>.Fail(ApiErrors.UnknownError) 
            : Result<EmployeeResponse>.Fail(new Error(problem!.Detail));
    }
}