using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class EmployeeService(IEmployeeHttpClient employeeHttpClient, IUserHttpClient userHttpClient)
    : IEmployeeService
{
    public async Task<Result<CreatedResponse>> Add(EmployeeAddRequest request)
        => await employeeHttpClient.PostEmployeeAsync(request);

    public async Task<Result<UpdatedResponse>> Update(Guid id, EmployeeUpdateRequest request)
        => await employeeHttpClient.PutEmployeeAsync(id, request);

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAll()
        => await employeeHttpClient.GetAllAsync();

    public async Task<Result<EmployeeResponse>> GetById(Guid id)
        => await employeeHttpClient.GetByIdAsync(id);

    public async Task<Result> DeleteById(Guid id)
        => await employeeHttpClient.DeleteByIdAsync(id);

    public async Task<Result<PagedList<EmployeeListResponse>>> GetList(EmployeeListQuery query)
        => await employeeHttpClient.GetListAsync(query);

    public async Task<Result<EmployeeProfileResponse>> GetSelf()
        => await employeeHttpClient.GetSelfAsync();

    public async Task<Result<UpdatedResponse>> UpdateAddress(EmployeeAddressUpdateRequest request)
        => await employeeHttpClient.PutEmployeeAddressAsync(request);

    public async Task<Result> ChangePassword(PasswordChangeRequest request)
        => await userHttpClient.PutPasswordAsync(request);
}