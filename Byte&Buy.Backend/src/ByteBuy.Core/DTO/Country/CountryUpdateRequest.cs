using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Country;

public record CountryUpdateRequest(
    [Required, MaxLength(50)] string Name,
    [Required, MaxLength(3)] string Code);