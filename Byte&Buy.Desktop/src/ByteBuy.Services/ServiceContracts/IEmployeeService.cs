using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.HttpClients.Implementations;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IEmployeeService
{
    Task<Result<IEnumerable<EmployeeListResponse>>> GetList();
    Task<Result<EmployeeResponse>> GetSelf();
    Task<Result> ChangePassword(PasswordChangeRequest request);
    Task<Result<UpdatedResponse>> UpdateEmployeeAddress(EmployeeAddressUpdateRequest request);
    Task<Result<CreatedResponse>> Add(EmployeeAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, EmployeeUpdateRequest request);
    Task<Result<IEnumerable<EmployeeResponse>>> GetAll();
    Task<Result<EmployeeResponse>> GetById(Guid id);
    Task<Result> DeleteById(Guid id);
}