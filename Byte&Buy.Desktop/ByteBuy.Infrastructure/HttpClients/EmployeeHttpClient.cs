using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class EmployeeHttpClient(HttpClient client)
    : HttpClientBase(client), IEmployeeHttpClient
{
    private const string resource = "employees";
    public async Task<Result<EmployeeProfileResponse>> GetSelfAsync()
        => await GetAsync<EmployeeProfileResponse>($"{resource}/me");

    public async Task<Result<UpdatedResponse>> PutEmployeeAddressAsync(EmployeeAddressUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/address", request);

    public Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync()
        => GetAsync<IEnumerable<EmployeeResponse>>($"{resource}");

    public async Task<Result<CreatedResponse>> PostEmployeeAsync(EmployeeAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public Task<Result<EmployeeResponse>> GetByIdAsync(Guid employeeId)
        => GetAsync<EmployeeResponse>($"{resource}/{employeeId}");

    public Task<Result<UpdatedResponse>> PutEmployeeAsync(Guid employeeId, EmployeeUpdateRequest request)
        => PutAsync<UpdatedResponse>($"{resource}/{employeeId}", request);

    public Task<Result> DeleteByIdAsync(Guid employeeId)
        => DeleteAsync($"{resource}/{employeeId}");

    public Task<Result<PagedList<EmployeeListResponse>>> GetListAsync(EmployeeListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return GetAsync<PagedList<EmployeeListResponse>>(url);
    }
}