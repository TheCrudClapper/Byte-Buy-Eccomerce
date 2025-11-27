using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IEmployeeHttpClient
{
    Task<Result<EmployeeResponse>> GetSelfAsync();
    Task<Result<EmployeeAddressResponse>> UpdateEmployeeAddressAsync(EmployeeAddressUpdateRequest request);
    Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync();
    Task<Result<EmployeeResponse>> AddEmployeeAsync(EmployeeAddRequest request);
    Task<Result<EmployeeResponse>> GetById(Guid id); 
    Task<Result<EmployeeResponse>> UpdateEmployeeAsync(Guid id, EmployeeUpdateRequest request);
    Task<Result> DeleteEmployeeByIdAsync(Guid id);
    Task<Result<IEnumerable<EmployeeListResponse>>> GetListAsync();
}