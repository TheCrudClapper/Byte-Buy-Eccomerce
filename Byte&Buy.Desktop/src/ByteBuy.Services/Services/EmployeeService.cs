using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class EmployeeService(IEmployeeHttpClient employeeHttpClient, IUserHttpClient userHttpClient)
    : IEmployeeService
{
    public async Task<Result<CreatedResponse>> Add(EmployeeAddRequest request)
        => await employeeHttpClient.AddEmployeeAsync(request);

    public async Task<Result<UpdatedResponse>> Update(Guid id, EmployeeUpdateRequest request)
        => await employeeHttpClient.UpdateEmployeeAsync(id, request);

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAll()
        => await employeeHttpClient.GetAllAsync();

    public async Task<Result<EmployeeResponse>> GetById(Guid id)
        => await employeeHttpClient.GetById(id);

    public async Task<Result> DeleteById(Guid id)
        => await employeeHttpClient.DeleteEmployeeByIdAsync(id);

    public async Task<Result<IEnumerable<EmployeeListResponse>>> GetList()
        => await employeeHttpClient.GetListAsync();

    public async Task<Result<EmployeeProfileResponse>> GetSelf()
        => await employeeHttpClient.GetSelfAsync();

    public async Task<Result<UpdatedResponse>> UpdateEmployeeAddress(EmployeeAddressUpdateRequest request)
        => await employeeHttpClient.UpdateEmployeeAddressAsync(request);

    public async Task<Result> ChangePassword(PasswordChangeRequest request)
        => await userHttpClient.ChangePasswordAsync(request);
}