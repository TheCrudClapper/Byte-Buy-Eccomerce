using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Employee;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class EmployeeSpecifications
{
    public sealed class EmployeeAggregateSpec : Specification<Employee>
    {
        public EmployeeAggregateSpec(Guid id)
        {
            Query.IgnoreQueryFilters()
                .Where(e => e.Id == id)
                .Include(e => e.UserPermissions);
        }
    }

    public sealed class EmployeeResponseSpec : Specification<Employee, EmployeeResponse>
    {
        public EmployeeResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(EmployeeMappings.EmployeeResponseProjection);
        }
    }

    public sealed class EmployeeProfileResponseSpec : Specification<Employee, EmployeeProfileResponse>
    {
        public EmployeeProfileResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(EmployeeMappings.EmployeeProfileResponseProjection);
        }
    }

}
