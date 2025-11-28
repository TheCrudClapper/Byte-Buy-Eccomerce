using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.Employee;

public record EmployeeAddressUpdateRequest(
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber,
    string? PhoneNumber
    );
