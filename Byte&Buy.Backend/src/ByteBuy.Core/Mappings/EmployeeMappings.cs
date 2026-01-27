using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Employee;
using ByteBuy.Core.DTO.Public.Shared;
using System.Linq.Expressions;

namespace ByteBuy.Core.Mappings;

public static class EmployeeMappings
{
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
            new HomeAddressDto(
                e.HomeAddress!.Street,
                e.HomeAddress.HouseNumber,
                e.HomeAddress.PostalCity,
                e.HomeAddress.PostalCode,
                e.HomeAddress.City,
                e.HomeAddress.Country,
                e.HomeAddress.FlatNumber),
            e.PhoneNumber!,
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
           e.PhoneNumber!,
           new HomeAddressDto(
                e.HomeAddress!.Street,
                e.HomeAddress.HouseNumber,
                e.HomeAddress.PostalCity,
                e.HomeAddress.PostalCode,
                e.HomeAddress.City,
                e.HomeAddress.Country,
                e.HomeAddress.FlatNumber)
           );

}
