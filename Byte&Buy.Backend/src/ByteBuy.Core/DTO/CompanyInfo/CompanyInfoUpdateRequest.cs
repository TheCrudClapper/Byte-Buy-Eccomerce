using ByteBuy.Core.DTO.AddressValueObj;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.CompanyInfo;
public record CompanyInfoUpdateRequest(
    [Required, MaxLength(50)] string CompanyName,
    [Required, MaxLength(20)] string TIN,
    [Required, MaxLength(50)] string Email,
    [Required, MaxLength(16)] string Phone,
    [MaxLength(30)] string? Slogan,
    [Required] AddressDto Address
);
