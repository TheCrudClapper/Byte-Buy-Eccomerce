using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

/// <summary>
/// Defines a contract for managing employee entities in a data store, including operations to add, update, retrieve,
/// and delete employees.
/// </summary>
/// <remarks>Implementations of this interface should provide asynchronous access to employee data and support
/// querying by identifier or custom conditions. Methods accept a <see cref="CancellationToken"/> to support
/// cancellation of long-running operations. This interface is used in repository patterns to abstract data
/// access logic and promote testability.</remarks>
public interface IEmployeeRepository : IRepositoryBase<Employee>
{
    Task<Employee?> GetByIdAsync(Guid employeeId, CancellationToken ct = default);
}
