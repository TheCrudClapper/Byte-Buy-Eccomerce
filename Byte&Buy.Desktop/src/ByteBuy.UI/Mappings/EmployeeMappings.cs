using ByteBuy.Services.DTO.Employee;
using ByteBuy.UI.ModelsUI.Employee;

namespace ByteBuy.UI.Mappings;

public static class EmployeeMappings
{
    public static EmployeeListItem ToListItem(this EmployeeListResponse employee, int index)
    {
        return new EmployeeListItem()
        {
            Id = employee.Id,
            RowNumber = index + 1,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Role = employee.Role,
            Email = employee.Email,
        };
    }
}