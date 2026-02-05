using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Money;

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