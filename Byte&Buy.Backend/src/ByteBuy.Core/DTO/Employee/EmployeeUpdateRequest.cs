using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Employee;

/// <summary>
/// Represents a request to update an employee's personal and contact information.
/// </summary>
public record EmployeeUpdateRequest(
    [Required] Guid RoleId,
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    [Required, MaxLength(50)] string Street,
    [Required, MaxLength(10)] string HouseNumber,
    [Required, MaxLength(20)] string PostalCode,
    [Required, MaxLength(50)] string City,
    [Required, MaxLength(50)] string Country,
    [MaxLength(10)] string? FlatNumber
    );