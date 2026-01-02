using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.Employee;

public record EmployeeResponse(
    Guid Id,
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    HomeAddressDto HomeAddress,
    string PhoneNumber,
    IReadOnlyCollection<Guid> GrantedPermissionIds,
    IReadOnlyCollection<Guid> RevokedPermissionIds
);

