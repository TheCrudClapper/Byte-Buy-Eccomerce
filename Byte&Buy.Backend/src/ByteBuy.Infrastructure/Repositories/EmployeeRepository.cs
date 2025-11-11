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
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees.ToListAsync();
    }

    public Task<IEnumerable<Employee>> GetAllByCondition(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByCondition(Expression<Func<Employee, bool>> expression, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> UpdateAsync(Guid employeeId, Employee employee, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
