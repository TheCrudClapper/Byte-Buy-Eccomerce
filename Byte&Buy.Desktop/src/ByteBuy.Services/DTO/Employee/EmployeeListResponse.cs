namespace ByteBuy.Services.DTO.Employee;

public record EmployeeListResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role
);
