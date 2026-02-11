namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record CompanyData
{
    public string CompanyName { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string TIN { get; init; } = null!;
    public AddressModel CompanyAddress { get; init; } = null!;
}
