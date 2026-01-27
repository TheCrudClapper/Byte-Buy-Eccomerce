using ByteBuy.Core.DTO.Public.Image;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using System.Text.Json.Serialization;

namespace ByteBuy.Core.DTO.Public.Offer.Common;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(SaleBrowserItemResponse), "sale")]
[JsonDerivedType(typeof(RentBrowserItemResponse), "rent")]
public abstract record OfferBrowserItemResponse
{
    public Guid Id { get; init; }
    public ImageResponse Image { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Condition { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string City { get; init; } = null!;
    public string PostalCity { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public bool IsCompanyOffer { get; init; }
}
