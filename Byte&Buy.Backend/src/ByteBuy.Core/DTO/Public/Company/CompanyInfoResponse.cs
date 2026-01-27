using ByteBuy.Core.DTO.Public.AddressValueObj;

namespace ByteBuy.Core.DTO.Public.CompanyInfo;

public record CompanyInfoResponse(
    string CompanyName,
    string TIN,
    string Email,
    string PhoneNumber,
    string Slogan,
    HomeAddressDto Address
);
