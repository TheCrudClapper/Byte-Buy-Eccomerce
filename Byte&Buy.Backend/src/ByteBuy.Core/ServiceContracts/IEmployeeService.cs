using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;
/// <summary>
/// Defines operations for managing employee records, including adding, retrieving, and updating employee information.
/// </summary>
/// <remarks>Implementations of this interface should ensure thread safety if accessed concurrently. All methods
/// are asynchronous and return a <see cref="Result{T}"/> indicating success or failure, along with the relevant
/// employee data. Cancellation tokens allow callers to cancel ongoing operations as needed.</remarks>
public interface IEmployeeService
{
    Task<Result<EmployeeProfileResponse>> GetEmployeeProfileInfo(Guid employeeId, CancellationToken ct = default);
    Task<Result<CreatedResponse>> AddEmployee(EmployeeAddRequest request);
    Task<Result<UpdatedResponse>> UpdateEmployee(Guid employeeId, EmployeeUpdateRequest request);
    Task<Result<UpdatedResponse>> UpdateEmployeeAddress(Guid employeeId, EmployeeAddressUpdateRequest request);
    Task<Result> DeleteEmployee(Guid employeeId);
    Task<Result<EmployeeResponse>> GetEmployee(Guid employeeId, CancellationToken ct = default);
    Task<Result<IEnumerable<EmployeeListResponse>>> GetEmployeesList(CancellationToken ct = default);
}
