using ByteBuy.Core.DTO.Image;
using ByteBuy.Core.DTO.Money;
using System.Text.Json.Serialization;

namespace ByteBuy.Core.DTO.Cart.CartItem;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(SaleCartOfferResponse), "sale")]
[JsonDerivedType(typeof(RentCartOfferResponse), "rent")]
public abstract record CartOfferResponse
{
    public Guid Id { get; init; }
    public ImageResponse Image { get; init; } = null!;
    public string Title { get; init; } = null!;
    public int Quantity { get; init; }
    public MoneyDto Subtotal { get; init; } = null!;
}

