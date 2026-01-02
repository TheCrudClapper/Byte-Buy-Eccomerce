using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.CompanyInfo;

public class CompanyInfoUpdateRequest
{
    public string Slogan { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string TIN { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public HomeAddressDto Address { get; set; } = null!;
}