namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record CompanyDocumentModel
{
    public string CompanyName { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string TIN { get; init; } = null!;
    public AddressDocumentModel CompanyAddress { get; init; } = null!;
}
