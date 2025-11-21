using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class EmployeeHttpClient(HttpClient client) : HttpClientBase(client), IEmployeeHttpClient
{
    public async Task<Result<EmployeeResponse>> GetSelfAsync()
        => await GetAsync<EmployeeResponse>("/employees/me");
}