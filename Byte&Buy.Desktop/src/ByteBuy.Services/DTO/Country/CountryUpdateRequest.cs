using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Country;

public record CountryUpdateRequest(
    string Name,
    string Code);