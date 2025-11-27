namespace ByteBuy.Core.DTO.Employee;

public record EmployeeListResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role
    );
