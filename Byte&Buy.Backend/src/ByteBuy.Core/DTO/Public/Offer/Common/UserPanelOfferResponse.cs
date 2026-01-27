using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;
using System.Text.Json.Serialization;

namespace ByteBuy.Core.DTO.Public.Offer.Common;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(UserSalePanelResponse), "sale")]
[JsonDerivedType(typeof(UserRentPanelResponse), "rent")]
public record UserPanelOfferResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public ImageResponse Image { get; init; } = null!;
    public DateTime DateCreated { get; init; }
    public DateTime? DateEdited { get; init; }
    public int QuantityAvaliable { get; init; }
}


public record UserSalePanelResponse : UserPanelOfferResponse
{
    public MoneyDto PricePerItem { get; init; } = null!;
}

public record UserRentPanelResponse : UserPanelOfferResponse
{
    public MoneyDto PricePerDay { get; init; } = null!;
    public int MaxRentalDays { get; set; }
}
