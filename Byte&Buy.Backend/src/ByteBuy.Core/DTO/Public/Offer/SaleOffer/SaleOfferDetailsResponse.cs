using ByteBuy.Core.Domain.Offers.Enums;
using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleOfferDetailsResponse(
    Guid Id,
    int QuantityAvaliable,
    MoneyDto PricePerItem,
    OfferStatus Status,
    string Condition,
    string Category,
    string Description,
    string Title,
    Object Seller,
    IReadOnlyCollection<ImageResponse> Images);