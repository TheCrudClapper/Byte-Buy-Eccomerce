using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class EmployeeHttpClient(HttpClient client) : HttpClientBase(client), IEmployeeHttpClient
{
    public async Task<Result<EmployeeResponse>> GetSelfAsync()
        => await GetAsync<EmployeeResponse>("employees/me");

    public async Task<Result<EmployeeAddressResponse>> UpdateEmployeeAddressAsync(EmployeeAddressUpdateRequest request)
        => await PutAsync<EmployeeAddressResponse>("employees/address", request);

    public Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync()
        => GetAsync<IEnumerable<EmployeeResponse>>("employees");

    public async Task<Result<EmployeeResponse>> AddEmployeeAsync(EmployeeAddRequest request)
        => await PostAsync<EmployeeResponse>("employees", request);

    public Task<Result<EmployeeResponse>> GetById(Guid id)
        => GetAsync<EmployeeResponse>($"employees/{id}");

    public Task<Result<EmployeeResponse>> UpdateEmployeeAsync(Guid id, EmployeeUpdateRequest request)
        => PutAsync<EmployeeResponse>("employees/{id}", request);

    public Task<Result> DeleteEmployeeByIdAsync(Guid id)
        => DeleteAsync($"employees/{id}");

    public Task<Result<IEnumerable<EmployeeListResponse>>> GetListAsync()
        => GetAsync<IEnumerable<EmployeeListResponse>>("employees/list");
}