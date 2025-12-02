using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context) { }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Employee>> GetAllByConditionAsync(Expression<Func<Employee, bool>> expression, CancellationToken ct)
    {
        return await _context.Employees
            .IgnoreQueryFilters()
            .Where(expression)
            .ToListAsync(ct);
    }

    public async Task<Employee?> GetByIdAsync(Guid employeeId, CancellationToken ct)
    {
        return await _context.Employees
            .Include(e => e.UserPermissions)
            .FirstOrDefaultAsync(e => e.Id == employeeId, ct);
    }

    public async Task<Employee?> GetAggregateAsync(Guid employeeId, CancellationToken ct)
    {
        return await _context.Employees
            .IgnoreQueryFilters()
            .Include(e => e.UserPermissions)
            .FirstOrDefaultAsync(e => e.Id == employeeId, ct);

    }

    public async Task<Employee?> GetByConditionAsync(Expression<Func<Employee, bool>> expression, CancellationToken ct)
    {
        return await _context.Employees
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(expression, ct);
    }

    public async Task<IEnumerable<Employee>> GetAllWithRolesAsync(CancellationToken ct)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync(ct);
    }

    public async Task<Employee?> GetWithRolesById(Guid employeeId, CancellationToken ct)
    {
        return await _context.Employees
             .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Include(u => u.UserPermissions)
                .FirstOrDefaultAsync(e => e.Id == employeeId, ct);
    }
}
