using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.Rental;

public class RentalListQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;
    public string? ItemName { get; init; }
    public string? BorrowerEmail { get; init; }
    public int? RentalDaysFrom { get; init; }
    public int? RentalDaysTo { get; init; }
    public DateTime? RentalStartPeriod { get; init; }
    public DateTime? RentalEndPeriod { get; init; }
}
