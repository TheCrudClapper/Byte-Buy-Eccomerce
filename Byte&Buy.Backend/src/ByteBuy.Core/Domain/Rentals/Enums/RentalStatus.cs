using System.ComponentModel;

namespace ByteBuy.Core.Domain.Rentals.Enums;

public enum RentalStatus
{
    // Rental was created after successfull delivery
    [Description("Created")]
    Created = 0,

    // Rented item is in client's within rental period
    [Description("Active")]
    Active = 1,

    // Rental period was violated, user still hasn't send item back
    [Description("Overdue")]
    Overdue = 2,

    // Rental was completed on given terms
    [Description("Completed")]
    Completed = 3,
}
