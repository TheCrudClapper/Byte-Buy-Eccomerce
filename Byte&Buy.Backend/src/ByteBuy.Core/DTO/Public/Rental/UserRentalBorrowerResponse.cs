using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Rental;

public sealed record UserRentalBorrowerResponse(
    Guid Id,
    RentalStatus Status,
    string ItemName,
    int Quantity,
    MoneyDto TotalPricePaid,
    ImageThumbnailDto Thumbnail,
    DateTime DateCreated,
    int RentalDays,
    string LenderName,
    DateTime StartingRentalDate,
    DateTime EndingRentalDate);