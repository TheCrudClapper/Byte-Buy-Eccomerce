using ByteBuy.Services.Stores.Abstractions;

namespace ByteBuy.Services.Stores.Implementations;

public class TokenStore : ITokenStore
{
    public string? AccessToken { get; set; }
}