using ByteBuy.Core.DTO.Public.ImageThumbnail;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order.Rent;
using ByteBuy.Core.DTO.Public.Order.Sale;
using System.Text.Json.Serialization;

namespace ByteBuy.Core.DTO.Public.Order.Common;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(UserSaleOrderLineResponse), "sale")]
[JsonDerivedType(typeof(UserRentOrderLineResponse), "rent")]
public abstract record UserOrderLineResponse
{
    public string ItemTitle { get; set; } = null!;
    public int Quantity { get; set; }
    public MoneyDto Total { get; set; } = null!;
    public ImageThumbnailDto Thumbnail { get; set; } = null!;
}
