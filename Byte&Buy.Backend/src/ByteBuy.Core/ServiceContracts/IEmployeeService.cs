using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;
/// <summary>
/// Defines operations for managing employee records, including adding, retrieving, and updating employee information.
/// </summary>
/// <remarks>Implementations of this interface should ensure thread safety if accessed concurrently. All methods
/// are asynchronous and return a <see cref="Result{T}"/> indicating success or failure, along with the relevant
/// employee data. Cancellation tokens allow callers to cancel ongoing operations as needed.</remarks>
public interface IEmployeeService : IBaseCrudService<Guid, EmployeeAddRequest, EmployeeUpdateRequest, EmployeeResponse>
{
    Task<Result<EmployeeProfileResponse>> GetEmployeeProfileInfoAsync(Guid employeeId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> UpdateEmployeeAddressAsync(Guid employeeId, EmployeeAddressUpdateRequest request);
    Task<Result<IEnumerable<EmployeeListResponse>>> GetEmployeesListAsync(CancellationToken ct = default);
}
