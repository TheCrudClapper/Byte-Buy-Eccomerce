using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Rental;

/// <summary>
/// Dto used in displaying a list of rentals from perspective of seller/lender
/// </summary>
public sealed record UserRentalLenderResponse(
    Guid Id,
    RentalStatus Status,
    string ItemName,
    int Quantity,
    MoneyDto TotalPricePaid,
    ImageThumbnailDto Thumbnail,
    DateTime DateCreated,
    int RentalDays,
    string BorrowerName,
    string BorrowerEmail,
    DateTime StartingRentalDate,
    DateTime EndingRentalDate);