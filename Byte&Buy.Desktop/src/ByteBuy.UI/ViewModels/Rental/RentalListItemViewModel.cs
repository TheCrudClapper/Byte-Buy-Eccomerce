using ByteBuy.UI.ViewModels.Base;
using System;
namespace ByteBuy.UI.ViewModels.Rental;

public sealed record RentalListItemViewModel : IListItemViewModel
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string Status { get; set; } = null!;
    public string ItemName { get; set; } = null!;
    public int Quantity { get; set; }
    public int RentalDays { get; set; }
    public string BorrowerEmail { get; set; } = null!;
    public DateTime StartingRentalDate { get; set; }
    public DateTime EndingRentalDate { get; set; }
}
