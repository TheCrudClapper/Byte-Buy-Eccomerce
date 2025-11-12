using ByteBuy.Core.DTO.AddressValueObj;

namespace ByteBuy.Core.DTO.CompanyInfo;

public record CompanyInfoResponse(
    string CompanyName,
    string TIN,
    string Email,
    string Phone,
    string? Slogan,
    AddressDto Address
);
