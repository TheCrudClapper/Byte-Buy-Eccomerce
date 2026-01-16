using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Offer.SaleOffer;

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