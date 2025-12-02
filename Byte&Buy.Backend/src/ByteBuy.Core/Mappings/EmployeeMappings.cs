using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Employee;

namespace ByteBuy.Core.Mappings;

public static class EmployeeMappings
{
    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        return new EmployeeResponse(
            employee.Id,
            employee.UserRoles?.FirstOrDefault()?.Role.Id ?? Guid.Empty,
            employee.FirstName,
            employee.LastName,
            employee.Email!,
            employee.HomeAddress.Street,
            employee.HomeAddress.HouseNumber,
            employee.HomeAddress.PostalCode,
            employee.HomeAddress.City,
            employee.HomeAddress.Country,
            employee.HomeAddress.FlatNumber,
            employee.PhoneNumber,
            employee.UserPermissions
                ?.Where(up => up.IsGranted)
                .Select(up => up.PermissionId)
                .ToList() ?? [],
            employee.UserPermissions
                ?.Where(up => !up.IsGranted)
                .Select(up => up.PermissionId)
                .ToList() ?? []);
    }

    public static EmployeeListResponse ToEmployeeListResponse(this Employee employee)
    {
        return new EmployeeListResponse(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Email!,
            employee.UserRoles?.FirstOrDefault()?.Role.Name ?? "Unknown"
            );
    }

    public static UpdatedResponse ToUpdatedResponse(this Employee employee)
        => new UpdatedResponse(employee.Id, employee.DateEdited!.Value);

    public static CreatedResponse ToCreatedResponse(this Employee employee)
        => new CreatedResponse(employee.Id, employee.DateCreated);
}
