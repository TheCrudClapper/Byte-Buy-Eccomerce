using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class EmployeeHttpClient(HttpClient client) : HttpClientBase(client), IEmployeeHttpClient
{
    public async Task<Result<EmployeeProfileResponse>> GetSelfAsync()
        => await GetAsync<EmployeeProfileResponse>("employees/me");

    public async Task<Result<UpdatedResponse>> PutEmployeeAddressAsync(EmployeeAddressUpdateRequest request)
        => await PutAsync<UpdatedResponse>("employees/address", request);

    public Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync()
        => GetAsync<IEnumerable<EmployeeResponse>>("employees");

    public async Task<Result<CreatedResponse>> PostEmployeeAsync(EmployeeAddRequest request)
        => await PostAsync<CreatedResponse>("employees", request);

    public Task<Result<EmployeeResponse>> GetByIdAsync(Guid id)
        => GetAsync<EmployeeResponse>($"employees/{id}");

    public Task<Result<UpdatedResponse>> PutEmployeeAsync(Guid id, EmployeeUpdateRequest request)
        => PutAsync<UpdatedResponse>($"employees/{id}", request);

    public Task<Result> DeleteByIdAsync(Guid id)
        => DeleteAsync($"employees/{id}");

    public Task<Result<IEnumerable<EmployeeListResponse>>> GetListAsync()
        => GetAsync<IEnumerable<EmployeeListResponse>>("employees/list");
}