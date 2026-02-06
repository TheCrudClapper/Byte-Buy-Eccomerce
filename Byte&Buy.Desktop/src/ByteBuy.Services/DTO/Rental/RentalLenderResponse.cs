using ByteBuy.Services.DTO.Image;
using ByteBuy.Services.DTO.Money;
using ByteBuy.Services.DTO.Rental.Enum;

namespace ByteBuy.Services.DTO.Rental;

public sealed record RentalLenderResponse(
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