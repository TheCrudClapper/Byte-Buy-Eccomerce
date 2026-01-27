using ByteBuy.Core.DTO.Public.AddressValueObj;

namespace ByteBuy.Core.DTO.Public.Employee;

public record EmployeeProfileResponse(
    Guid Id,
    string RoleName,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    HomeAddressDto HomeAddress
);

