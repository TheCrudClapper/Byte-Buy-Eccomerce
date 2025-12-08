using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class EmployeeSpecifications
{
    public sealed class EmployeeWithRolesAndPermissionsSpec : Specification<Employee>
    {
        public EmployeeWithRolesAndPermissionsSpec(Guid id)
        {
            Query.Where(e => e.Id == id)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Include(u => u.UserPermissions);
        }
    }

    public sealed class EmployeeAggregateSpec : Specification<Employee>
    {
        public EmployeeAggregateSpec(Guid id)
        {
            Query.IgnoreQueryFilters()
                .Where(e => e.Id == id)
                .Include(e => e.UserPermissions);
        }
    }

    public sealed class EmployeeToEmployeeListDtoSpec : Specification<Employee, EmployeeListResponse>
    {
        public EmployeeToEmployeeListDtoSpec()
        {
            Query.AsNoTracking()
                .Select(EmployeeMappings.EmployeeListProjection);
        }
    }

    public sealed class EmployeeToEmployeeResponseDtoSpec : Specification<Employee, EmployeeResponse>
    {
        public EmployeeToEmployeeResponseDtoSpec(Guid id)
        {
            Query.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(EmployeeMappings.EmployeeResponseProjection);
        }
    }

    public sealed class EmployeeToEmployeeProfileResponseDto : Specification<Employee, EmployeeProfileResponse>
    {
        public EmployeeToEmployeeProfileResponseDto(Guid id)
        {
            Query.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(EmployeeMappings.EmployeeProfileResponseProjection);
        }
    }

}
