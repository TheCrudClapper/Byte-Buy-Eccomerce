using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.InfraContracts.HttpClients.Me;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class EmployeeService(ICompanyEmployeeHttpClient employeeHttpClient, IUserPasswordHttpClient userHttpClient)
    : IEmployeeService
{
    public async Task<Result<CreatedResponse>> AddAsync(EmployeeAddRequest request)
        => await employeeHttpClient.PostEmployeeAsync(request);

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, EmployeeUpdateRequest request)
        => await employeeHttpClient.PutEmployeeAsync(id, request);

    public async Task<Result<IReadOnlyCollection<EmployeeResponse>>> GetListAsync()
        => await employeeHttpClient.GetAllAsync();

    public async Task<Result<EmployeeResponse>> GetByIdAsync(Guid id)
        => await employeeHttpClient.GetByIdAsync(id);

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await employeeHttpClient.DeleteByIdAsync(id);

    public async Task<Result<PagedList<EmployeeListResponse>>> GetListAsync(EmployeeListQuery query)
        => await employeeHttpClient.GetListAsync(query);

    public async Task<Result<EmployeeProfileResponse>> GetCurrentEmployeeDetailsAsync()
        => await employeeHttpClient.GetEmployeeProfileDataAsync();

    public async Task<Result<UpdatedResponse>> UpdateCurrentEmployeeAddressAsync(EmployeeAddressUpdateRequest request)
        => await employeeHttpClient.PutEmployeeAddressAsync(request);

    public async Task<Result> ChangePasswordAsync(PasswordChangeRequest request)
        => await userHttpClient.PutPasswordAsync(request);
}