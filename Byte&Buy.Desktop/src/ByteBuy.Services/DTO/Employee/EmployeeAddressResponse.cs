namespace ByteBuy.Core.DTO.Employee;

public record EmployeeAddressResponse(
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber,
    string? PhoneNumber
    );
