namespace ByteBuy.Services.Filtration;

public class RentalListQuery
{
    public string? ItemName { get; init; }
    public string? BorrowerEmail { get; init; }
    public int? RentalDaysFrom { get; init; }
    public int? RentalDaysTo { get; init; }
    public DateTime? RentalStartPeriod { get; init; }
    public DateTime? RentalEndPeriod { get; init; }
}
