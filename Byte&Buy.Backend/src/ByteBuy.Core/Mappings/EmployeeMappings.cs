using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Employee;

namespace ByteBuy.Core.Mappings;

public static class EmployeeMappings
{
    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        return new EmployeeResponse(
            employee.FirstName,
            employee.LastName,
            employee.Email!,
            employee.HomeAddress.Street,
            employee.HomeAddress.HouseNumber,
            employee.HomeAddress.PostalCode,
            employee.HomeAddress.City,
            employee.HomeAddress.Country,
            employee.HomeAddress.FlatNumber
            );
    }
}
