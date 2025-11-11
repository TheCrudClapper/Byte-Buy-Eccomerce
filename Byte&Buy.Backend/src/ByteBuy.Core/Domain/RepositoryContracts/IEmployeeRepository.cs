using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

/// <summary>
/// Defines a contract for managing employee entities in a data store, including operations to add, update, retrieve,
/// and delete employees.
/// </summary>
/// <remarks>Implementations of this interface should provide asynchronous access to employee data and support
/// querying by identifier or custom conditions. Methods accept a <see cref="CancellationToken"/> to support
/// cancellation of long-running operations. This interface is typically used in repository patterns to abstract data
/// access logic and promote testability.</remarks>
public interface IEmployeeRepository
{
    Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<Employee> UpdateAsync(Guid employeeId, Employee employee, CancellationToken cancellationToken = default);
    Task<Employee> GetAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Employee>> GetAllByCondition(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken = default);
    Task<Employee> GetByCondition(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken = default);
}
