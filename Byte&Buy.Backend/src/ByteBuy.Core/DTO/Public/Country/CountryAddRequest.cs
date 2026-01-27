using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Country;

public record CountryAddRequest(
    [Required, MaxLength(50)] string Name,
    [Required, MaxLength(3)] string Code);