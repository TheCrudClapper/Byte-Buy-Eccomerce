using ByteBuy.Services.DTO.Employee;
using ByteBuy.Services.ModelsUI.Employee;

namespace ByteBuy.Services.Mappings;

public static class EmployeeMappings
{
    public static EmployeeListItem ToEmployeeListItem(this EmployeeListResponse employee)
    {
        return new EmployeeListItem
        {
            Email = employee.Email,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Role = employee.Role
        };

    }
}
