using ByteBuy.Core.DTO.AddressValueObj;

namespace ByteBuy.Core.DTO.Employee;

public record EmployeeProfileResponse(
    Guid Id,
    string RoleName,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    HomeAddressDto HomeAddress
);

