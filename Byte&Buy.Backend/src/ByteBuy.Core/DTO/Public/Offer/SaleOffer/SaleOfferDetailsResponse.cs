using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleOfferDetailsResponse(
    Guid Id,
    int QuantityAvaliable,
    MoneyDto PricePerItem,
    string Condition,
    string Category,
    string Description,
    string Title,
    Object Seller,
    IReadOnlyCollection<ImageResponse> Images);