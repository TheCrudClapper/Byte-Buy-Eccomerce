using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class EmployeeService(IEmployeeHttpClient employeeHttpClient, IUserHttpClient userHttpClient) 
    : CrudServiceBase<EmployeeResponse, EmployeeAddRequest, EmployeeUpdateRequest> ,IEmployeeService
{
    public override async Task<Result<EmployeeResponse>> Add(EmployeeAddRequest request)
        => await employeeHttpClient.AddEmployeeAsync(request);

    public override Task<Result<EmployeeResponse>> Update(Guid id, EmployeeUpdateRequest request) 
        =>  throw new NotImplementedException();

    public override async Task<Result<IEnumerable<EmployeeResponse>>> GetAll()
        => await employeeHttpClient.GetAllAsync();

    public override Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => throw new NotImplementedException();

    public override Task<Result<EmployeeResponse>> GetById(Guid id)
        => throw new NotImplementedException();

    public override Task<Result> DeleteById(Guid id)
        => throw new NotImplementedException();
    
    public async Task<Result<EmployeeResponse>> GetSelf()
        => await employeeHttpClient.GetSelfAsync();

    public async Task<Result<EmployeeAddressResponse>> UpdateEmployeeAddress(EmployeeAddressUpdateRequest request)
        => await employeeHttpClient.UpdateEmployeeAddressAsync(request);
    
    public async Task<Result> ChangePassword(PasswordChangeRequest request)
        => await userHttpClient.ChangePasswordAsync(request);
}