using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context){}

    public async Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return employee;
    }

    public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Where(e => e.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetAllByConditionAsync(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.Where(expression)
            .ToListAsync(cancellationToken);    
    }

    public async Task<Employee?> GetAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == employeeId && e.IsActive, cancellationToken);
    }

    public async Task<Employee?> GetByConditionAsync(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return employee;
    }
}
