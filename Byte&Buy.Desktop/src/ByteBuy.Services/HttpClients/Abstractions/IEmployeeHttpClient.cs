using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IEmployeeHttpClient
{
    Task<Result<EmployeeResponse>> GetSelfAsync();
    Task<Result<UpdatedResponse>> UpdateEmployeeAddressAsync(EmployeeAddressUpdateRequest request);
    Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync();
    Task<Result<CreatedResponse>> AddEmployeeAsync(EmployeeAddRequest request);
    Task<Result<EmployeeResponse>> GetById(Guid id); 
    Task<Result<UpdatedResponse>> UpdateEmployeeAsync(Guid id, EmployeeUpdateRequest request);
    Task<Result> DeleteEmployeeByIdAsync(Guid id);
    Task<Result<IEnumerable<EmployeeListResponse>>> GetListAsync();
}