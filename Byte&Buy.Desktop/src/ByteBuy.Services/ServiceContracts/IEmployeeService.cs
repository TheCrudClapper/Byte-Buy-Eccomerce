using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IEmployeeService
{
    Task<Result<EmployeeResponse>> GetSelf();
    Task<Result<IEnumerable<EmployeeResponse>>> GetAll(); 
    Task<Result> ChangePassword(PasswordChangeRequest request);
    Task<Result<EmployeeAddressResponse>> UpdateEmployeeAddress(EmployeeAddressUpdateRequest request);
    Task<Result<EmployeeResponse>> AddEmployee(EmployeeAddRequest request);
}