using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.Employee;

public sealed class EmployeeListQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
}
