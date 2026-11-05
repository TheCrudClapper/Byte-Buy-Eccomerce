using ByteBuy.Services.InfraContracts.Stores;

namespace ByteBuy.Infrastructure.Stores;

public class TokenStore : ITokenStore
{
    public string? AccessToken { get => _token; }
    private string? _token;
    public void ClearToken()
    {
        _token = null;
    }

    public void SetToken(string token)
    {
        _token = token;
    }
}