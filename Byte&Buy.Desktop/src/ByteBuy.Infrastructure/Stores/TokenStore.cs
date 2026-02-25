using ByteBuy.Services.InfraContracts.Stores;

namespace ByteBuy.Infrastructure.Stores;

public class TokenStore : ITokenStore
{
    public string? AccessToken { get; set; }
}