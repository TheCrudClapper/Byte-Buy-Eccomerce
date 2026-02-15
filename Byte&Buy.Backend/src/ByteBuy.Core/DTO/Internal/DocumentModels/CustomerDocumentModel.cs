namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record CustomerDocumentModel
{
    public string CustomersFullName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public AddressDocumentModel CustomerAddress { get; init; } = null!;
}
