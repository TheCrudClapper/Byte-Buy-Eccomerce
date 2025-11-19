using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

/// <summary>
/// Defines a contract for managing employee entities in a data store, including operations to add, update, retrieve,
/// and delete employees.
/// </summary>
/// <remarks>Implementations of this interface should provide asynchronous access to employee data and support
/// querying by identifier or custom conditions. Methods accept a <see cref="CancellationToken"/> to support
/// cancellation of long-running operations. This interface is used in repository patterns to abstract data
/// access logic and promote testability.</remarks>
public interface IEmployeeRepository
{
    Task UpdateAsync(Employee employee, CancellationToken ct = default);
    Task<Employee?> GetByIdAsync(Guid employeeId, CancellationToken ct = default);
    Task SoftDeleteAsync(Employee employee, CancellationToken ct = default);
    Task<IEnumerable<Employee>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Employee>> GetAllByConditionAsync(Expression<Func<Employee, bool>> expression, CancellationToken ct = default);
    Task<Employee?> GetByConditionAsync(Expression<Func<Employee, bool>> expression, CancellationToken ct = default);
    
}
 