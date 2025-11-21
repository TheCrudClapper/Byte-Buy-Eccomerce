using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IEmployeeService
{
    Task<Result<EmployeeResponse>> GetSelf();
}