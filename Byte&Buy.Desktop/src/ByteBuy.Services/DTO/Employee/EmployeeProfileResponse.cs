using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.Employee;

public record EmployeeProfileResponse(
    Guid Id,
    string RoleName,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    HomeAddressDto HomeAddress
);