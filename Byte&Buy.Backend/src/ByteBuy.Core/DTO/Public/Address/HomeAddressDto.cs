using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.AddressValueObj;

/// <summary>
/// Represents a data transfer object containing address information, including street, house number, postal code, city,
/// country, and optional flat number.
/// </summary>
public record HomeAddressDto(
    [Required, MaxLength(50)] string Street,
    [Required, MaxLength(10)] string HouseNumber,
    [Required, MaxLength(50)] string PostalCity,
    [Required, MaxLength(20)] string PostalCode,
    [Required, MaxLength(50)] string City,
    [Required, MaxLength(50)] string Country,
    [MaxLength(10)] string? FlatNumber
);
