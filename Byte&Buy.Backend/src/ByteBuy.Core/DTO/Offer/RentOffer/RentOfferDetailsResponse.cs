using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Offer.RentOffer;

public record RentOfferDetailsResponse(
    Guid Id,
    int MaxRentalDays,
    int QuantityAvaliable,
    MoneyDto PricePerDay,
    string Condition,
    string Category,
    string Description,
    string Title,
    Object Seller,
    IReadOnlyCollection<ImageResponse> Images);