using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Public.Cart.Enum;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Offer.Enum;
namespace ByteBuy.Core.DTO.Internal.Cart;

public record FlatCartOffersQuery
{
    public Guid OfferId { get; init; }
    public Guid SellerId { get; init; }
    public ImageThumbnail Thumbnail { get; init; } = null!;
    public string Title { get; init; } = null!;
    public int Quantity { get; init; }
    public int? RentalDays { get; init; }
    public MoneyDto? PricePerDay { get; init; }
    public MoneyDto? PricePerItem { get; init; }
    public CartOfferType Type { get; init; }
    public SellerType SellerType { get; init; }
}
