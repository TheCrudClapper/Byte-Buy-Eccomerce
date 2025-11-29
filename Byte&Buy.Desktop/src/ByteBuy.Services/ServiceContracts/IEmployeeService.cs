using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IEmployeeService
{
    Task<Result<IEnumerable<EmployeeListResponse>>> GetList();
    Task<Result<EmployeeResponse>> GetSelf();
    Task<Result> ChangePassword(PasswordChangeRequest request);
    Task<Result<EmployeeAddressResponse>> UpdateEmployeeAddress(EmployeeAddressUpdateRequest request);
}