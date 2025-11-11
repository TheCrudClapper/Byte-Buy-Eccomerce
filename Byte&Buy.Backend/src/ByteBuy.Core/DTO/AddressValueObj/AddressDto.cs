using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.AddressValueObj;

/// <summary>
/// Represents a data transfer object containing address information, including street, house number, postal code, city,
/// country, and optional flat number.
/// </summary>
public record AddressDto(
    [Required, MaxLength(50)] string Street,
    [Required, MaxLength(20)] string HouseNumber,
    [Required, MaxLength(50)] string PostalCode,
    [Required, MaxLength(50)] string City,
    [Required, MaxLength(50)] string Country,
    [MaxLength(10)] string? FlatNumber
);
