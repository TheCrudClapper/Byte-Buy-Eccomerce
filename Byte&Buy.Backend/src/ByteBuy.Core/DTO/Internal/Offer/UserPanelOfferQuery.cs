using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Offer.Enum;

namespace ByteBuy.Core.DTO.Internal.Offer;

//Query object that is used for fetching all types of offers in one go.
public record UserPanelOfferQuery
{
    public Guid Id { get; init; }
    public OfferType Type { get; init; }
    public OfferStatus Status { get; init; }
    public string Title { get; init; } = null!;
    public ImageResponse Image { get; init; } = null!;
    public DateTime DateCreated { get; init; }
    public DateTime? DateEdited { get; init; }
    public int QuantityAvaliable { get; init; }
    public MoneyDto? PricePerItem { get; init; }
    public MoneyDto? PricePerDay { get; init; }
    public int? MaxRentalDays { get; set; }
}
