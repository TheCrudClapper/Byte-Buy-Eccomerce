using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Employee;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class EmployeeMappings
{
    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        var roles = employee.UserRoles;
        var roleId = roles?.FirstOrDefault()?.Role?.Id ?? Guid.Empty;

        var permissions = employee.UserPermissions ?? [];
        var grantedPermissions = permissions
            .Where(p => p.IsGranted)
            .Select(p => p.PermissionId)
            .ToList();

        var revokedPermissions = permissions
            .Where(p => !p.IsGranted)
            .Select(p => p.PermissionId)
            .ToList();

        return new EmployeeResponse(
            employee.Id,
            roleId,
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
            grantedPermissions,
            revokedPermissions);
    }

    public static EmployeeProfileResponse ToEmployeeProfileResponse(this Employee employee)
    {
        return new EmployeeProfileResponse(
            employee.Id,
            employee.UserRoles?.FirstOrDefault()?.Role.Name ?? "Unknown",
            employee.FirstName,
            employee.LastName,
            employee.Email!,
            employee.HomeAddress.Street,
            employee.HomeAddress.HouseNumber,
            employee.HomeAddress.PostalCode,
            employee.HomeAddress.City,
            employee.HomeAddress.Country,
            employee.HomeAddress.FlatNumber,
            employee.PhoneNumber
            );
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

    //Ef projections
    public static Expression<Func<Employee, EmployeeListResponse>> EmployeeListProjection =>
        e => new EmployeeListResponse(
            e.Id,
            e.FirstName,
            e.LastName,
            e.Email!,
            e.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault() ?? "Unknown"
            );

    public static Expression<Func<Employee, EmployeeResponse>> EmployeeResponseProjection =>
        e => new EmployeeResponse(
            e.Id,
            e.UserRoles
                .Select(ur => (Guid?)ur.RoleId)
                .FirstOrDefault()
                .GetValueOrDefault(),
            e.FirstName,
            e.LastName,
            e.Email!,
            e.HomeAddress.Street,
            e.HomeAddress.HouseNumber,
            e.HomeAddress.PostalCode,
            e.HomeAddress.City,
            e.HomeAddress.Country,
            e.HomeAddress.FlatNumber,
            e.PhoneNumber,
            e.UserPermissions
                .Where(up => up.IsGranted)
                .Select(up => up.PermissionId)
                .ToList(),
            e.UserPermissions
                .Where(up => !up.IsGranted)
                .Select(up => up.PermissionId)
                .ToList()
            );

    public static Expression<Func<Employee, EmployeeProfileResponse>> EmployeeProfileResponseProjection =>
       e => new EmployeeProfileResponse(
           e.Id,
           e.UserRoles
               .Select(ur => ur.Role.Name)
               .FirstOrDefault() ?? "Unknown",
           e.FirstName,
           e.LastName,
           e.Email!,
           e.HomeAddress.Street,
           e.HomeAddress.HouseNumber,
           e.HomeAddress.PostalCode,
           e.HomeAddress.City,
           e.HomeAddress.Country,
           e.HomeAddress.FlatNumber,
           e.PhoneNumber
           );

}
