using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Address;

public record AddressUpdateRequest(
    [Required, MaxLength(50)] string Label,
    [Required, MaxLength(50)] string Street,
    [Required, MaxLength(10)] string HouseNumber,
    [Required, MaxLength(20)] string PostalCode,
    [Required, MaxLength(50)] string City,
    [Required, MaxLength(50)] string Country,
    [MaxLength(10)] string? FlatNumber
    );

