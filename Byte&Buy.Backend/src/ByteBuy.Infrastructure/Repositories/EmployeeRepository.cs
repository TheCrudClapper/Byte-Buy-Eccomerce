using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class EmployeeRepository : EfBaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Employee?> GetByIdAsync(Guid employeeId, CancellationToken ct)
    {
        return await _context.Employees
            .Include(e => e.UserPermissions)
            .FirstOrDefaultAsync(e => e.Id == employeeId, ct);
    }
}
