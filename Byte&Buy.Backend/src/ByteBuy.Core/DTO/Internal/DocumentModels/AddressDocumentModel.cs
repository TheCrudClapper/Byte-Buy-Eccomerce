namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record AddressDocumentModel
{
    public string City { get; init; } = null!;
    public string Street { get; init; } = null!;
    public string HouseNumber { get; init; } = null!;
    public string? FlatNumber { get; init; } = null!;
    public string PostalCity { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public string Country { get; init; } = null!;
}
