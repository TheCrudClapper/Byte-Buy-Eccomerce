using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.Employee;

public record EmployeeAddressUpdateRequest(
    HomeAddressDto HomeAddress,
    string PhoneNumber);
