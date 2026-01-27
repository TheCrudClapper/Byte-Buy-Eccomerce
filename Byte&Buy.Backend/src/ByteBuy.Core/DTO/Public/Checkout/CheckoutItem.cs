using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Public.Cart.CartOffer;
using ByteBuy.Core.DTO.Public.Money;
using System.Text.Json.Serialization;

namespace ByteBuy.Core.DTO.Public.Checkout;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(SaleCheckoutItem), "sale")]
[JsonDerivedType(typeof(RentCheckoutItem), "rent")]
public abstract record CheckoutItem
{
    public Guid OfferId { get; init; }
    public string ItemName { get; init; } = null!;
    public ImageThumbnail Thumbnail { get; init; } = null!;
    public int Quantity { get; init; }
    public MoneyDto Subtotal { get; init; } = null!;
}


