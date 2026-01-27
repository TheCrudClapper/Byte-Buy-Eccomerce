using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

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