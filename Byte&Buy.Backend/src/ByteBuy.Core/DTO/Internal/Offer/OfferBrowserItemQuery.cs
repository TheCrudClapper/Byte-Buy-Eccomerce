using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Offer.Enum;

namespace ByteBuy.Core.DTO.Internal.Offer;

//Query object that is used for fetching all types of offers in one go.
public record OfferBrowserItemQuery
{
    public Guid Id { get; init; }
    public OfferType Type { get; init; }
    public ImageResponse Image { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Condition { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string City { get; init; } = null!;
    public OfferStatus Status { get; init; }
    public string PostalCity { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public bool IsCompanyOffer { get; init; }
    public int? MaxRentalDays { get; init; }
    public MoneyDto? PricePerDay { get; init; } = null!;
    public MoneyDto? PricePerItem { get; init; } = null!;
}
