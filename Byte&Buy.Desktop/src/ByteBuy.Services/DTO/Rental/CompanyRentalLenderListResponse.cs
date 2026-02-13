using ByteBuy.Services.DTO.Rental.Enum;

namespace ByteBuy.Core.DTO.Public.Rental;

public sealed record CompanyRentalLenderListResponse(
    Guid Id,
    RentalStatus Status,
    string ItemName,
    int Quantity,
    int RentalDays,
    string BorrowerEmail,
    DateTime StartingRentalDate,
    DateTime EndingRentalDate);