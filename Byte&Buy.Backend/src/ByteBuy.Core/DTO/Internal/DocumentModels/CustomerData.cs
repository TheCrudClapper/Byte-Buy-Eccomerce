namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record CustomerData
{
    public string FirstName{ get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public AddressModel CustomerAddress { get; init; } = null!;
}
