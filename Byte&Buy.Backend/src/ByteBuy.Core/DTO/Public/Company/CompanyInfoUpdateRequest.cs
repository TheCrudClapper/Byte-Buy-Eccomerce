using ByteBuy.Core.DTO.Public.AddressValueObj;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.CompanyInfo;

public record CompanyInfoUpdateRequest(
    [Required, MaxLength(50)] string CompanyName,
    [Required, MaxLength(20)] string TIN,
    [Required, MaxLength(50)] string Email,
    [Required, MaxLength(16)] string PhoneNumber,
    [MaxLength(30)] string Slogan,
    [Required] HomeAddressDto Address
);
