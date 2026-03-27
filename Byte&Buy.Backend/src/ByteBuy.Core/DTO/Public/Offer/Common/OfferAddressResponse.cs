namespace ByteBuy.Core.DTO.Public.Offer.Common;

/// <summary>
/// Record representing future or present address of and offer
/// </summary>
/// <param name="City"></param>
/// <param name="PostalCode"></param>
/// <param name="PostaCity"></param>
public record OfferAddressResponse(
    string City,
    string PostalCode,
    string PostalCity);