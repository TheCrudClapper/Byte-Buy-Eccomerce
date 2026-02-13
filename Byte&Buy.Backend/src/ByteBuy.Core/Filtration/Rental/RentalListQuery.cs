namespace ByteBuy.Core.Filtration.Rental;

public class RentalListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? ItemName { get; init; }
    public string? BorrowerEmail { get; init; }
    public int? RentalDaysFrom { get; init; }
    public int? RentalDaysTo { get; init; }
    public DateTime? RentalStartPeriod { get; init; }
    public DateTime? RentalEndPeriod { get; init; }
}
