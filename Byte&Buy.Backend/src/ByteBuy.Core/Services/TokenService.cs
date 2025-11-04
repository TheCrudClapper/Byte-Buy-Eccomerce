using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class TokenService : ITokenService
{
    public string GenerateJwtToken(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken(ApplicationUser user)
    {
        throw new NotImplementedException();
    }
}
