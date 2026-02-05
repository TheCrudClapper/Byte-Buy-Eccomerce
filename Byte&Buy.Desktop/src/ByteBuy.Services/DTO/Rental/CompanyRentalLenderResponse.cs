using ByteBuy.Services.DTO.Rental.Enum;

namespace ByteBuy.Core.DTO.Public.Rental;

public sealed record CompanyRentalLenderResponse(
    Guid Id,
    RentalStatus Status,
    string ItemName,
    int Quantity,
    int RentalDays,
    string BorrowerEmail,
    DateTime StartingRentalDate,
    DateTime EndingRentalDate);