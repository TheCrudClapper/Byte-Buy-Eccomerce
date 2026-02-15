using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Employee;
using ByteBuy.Core.Filtration.Employee;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class EmployeeRepository : EfBaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context) { }

    public override async Task<Employee?> GetByIdAsync(Guid employeeId, CancellationToken ct)
    {
        return await _context.Employees
            .Include(e => e.UserPermissions)
            .FirstOrDefaultAsync(e => e.Id == employeeId, ct);
    }

    public Task<PagedList<EmployeeListResponse>> GetEmployeePagedListAsync(Guid excludedUserId, EmployeeListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Employees
            .AsNoTracking()
            .Where(e => e.Id != excludedUserId)
            .OrderByDescending(e => e.DateCreated)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.FirstName))
            query = query.Where(e => EF.Functions.ILike(e.FirstName, $"%{queryParams.FirstName}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.Email))
            query = query.Where(e => EF.Functions.ILike(e.Email!, $"%{queryParams.Email}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.LastName))
            query = query.Where(e => EF.Functions.ILike(e.LastName, $"%{queryParams.LastName}%"));

        var projection = query.Select(EmployeeMappings.EmployeeListProjection);

        return projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }
}
