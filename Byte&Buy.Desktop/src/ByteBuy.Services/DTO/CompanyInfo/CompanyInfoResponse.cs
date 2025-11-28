using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.CompanyInfo;

public record CompanyInfoResponse(
    string CompanyName,
    string TIN,
    string Email,
    string PhoneNumber,
    string Slogan,
    AddressDto Address
);