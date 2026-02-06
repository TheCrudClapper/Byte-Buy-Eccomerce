using ByteBuy.Services.DTO.Image;
using ByteBuy.Services.DTO.Money;
using System.Text.Json.Serialization;

namespace ByteBuy.Services.DTO.Order.OrderLine;

public record UserSaleOrderLineResponse : UserOrderLineResponse
{
    public MoneyDto PricePerItem { get; set; } = null!;
}

public record UserRentOrderLineResponse : UserOrderLineResponse
{
    public MoneyDto PricePerDay { get; set; } = null!;
    public int RentalDays { get; set; }
}


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
