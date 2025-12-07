using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ByteBuy.Core.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateJwtToken(ApplicationUser user, IList<string> roles)
    {
        //Create symmetric security key
        var signinKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        //Creating signing credentials out of key and using HmacSha256
        var credentials = new SigningCredentials(signinKey,
            SecurityAlgorithms.HmacSha256);

        //Piling up users claims
        List<Claim> claims =
        [
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email!),
        ];

        foreach (var claim in roles)
        {
            claims.Add(new Claim("Roles", claim));
        }

        //Creating token payload
        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var tokenHandler = new JsonWebTokenHandler();

        //Creating token
        var accessToken = tokenHandler.CreateToken(tokenDescriptior);

        return accessToken;
    }

    public string GenerateRefreshToken(ApplicationUser user)
    {
        throw new NotImplementedException();
    }
}
