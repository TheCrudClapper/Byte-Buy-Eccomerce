using ByteBuy.Core.DTO.AddressValueObj;

namespace ByteBuy.Core.DTO.CompanyInfo;

public record CompanyInfoResponse(
    string CompanyName,
    string TIN,
    string Email,
    string PhoneNumber,
    string Slogan,
    HomeAddressDto Address
);
