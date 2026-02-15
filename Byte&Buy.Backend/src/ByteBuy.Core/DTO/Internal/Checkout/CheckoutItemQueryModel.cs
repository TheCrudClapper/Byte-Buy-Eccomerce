using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Internal.Cart.Enum;
using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Internal.Checkout;

public sealed record CheckoutItemQueryModel
{
    public Guid OfferId { get; init; }
    public Guid SellerId { get; init; }
    public ImageThumbnailDto Thumbnail { get; init; } = null!;
    public OfferStatus Status { get; init; }
    public string Title { get; init; } = null!;
    public int Quantity { get; init; }
    public int AvaliableQuantity { get; init; }
    public int? RentalDays { get; init; }
    public MoneyDto? PricePerDay { get; init; }
    public MoneyDto? PricePerItem { get; init; }
    public CartOfferType Type { get; init; }
    public SellerType SellerType { get; init; }
    public IReadOnlyCollection<Guid> AvaliableDeliveriesIds { get; init; } = [];
    public bool CanFinalize { get; init; }
}
