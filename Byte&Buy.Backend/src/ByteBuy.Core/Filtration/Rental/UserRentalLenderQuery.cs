using ByteBuy.Core.Domain.Rentals.Enums;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.Rental;

public sealed class UserRentalLenderQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;

    public string? ItemName { get; init; }
    public DateTime? RentalStartDate { get; init; }
    public DateTime? RentalEndDate { get; init; }
    public RentalStatus? RentalStatus { get; init; }
}
