namespace ByteBuy.Services.DTO.Address;

public record HomeAddressDto
{
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string PostalCity { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? FlatNumber { get; set; } = null!;
}
