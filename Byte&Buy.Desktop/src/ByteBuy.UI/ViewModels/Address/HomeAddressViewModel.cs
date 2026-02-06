using ByteBuy.Services.DTO.Address;

namespace ByteBuy.UI.ViewModels.Address;

public class HomeAddressViewModel
{
    public string Street { get; } = null!;
    public string HouseNumber { get; } = null!;
    public string PostalCode { get; } = null!;
    public string PostalCity { get; } = null!;
    public string City { get; } = null!;
    public string Country { get; } = null!;
    public string? FlatNumber { get;} = null!;

    public HomeAddressViewModel(HomeAddressDto dto)
    {
        Street = dto.Street;
        HouseNumber = dto.HouseNumber;
        PostalCode = dto.PostalCode;
        PostalCity = dto.PostalCity;    
        City = dto.City;
        Country = dto.Country;
        FlatNumber = dto.FlatNumber is not null ?  $"/ {dto.FlatNumber}" : ""; 
    }
}
