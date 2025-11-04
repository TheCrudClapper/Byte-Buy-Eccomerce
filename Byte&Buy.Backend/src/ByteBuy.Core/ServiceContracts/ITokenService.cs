using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.ServiceContracts;

/// <summary>
/// Defines methods for generating JSON Web Tokens (JWT) and refresh tokens for authenticated users.
/// </summary>
/// <remarks>Implementations of this interface are responsible for creating secure tokens that can be used for
/// user authentication and session management. The generated tokens should be unique per user and conform to security
/// best practices.</remarks>
public interface ITokenService
{
    /// <summary>
    /// Generates a JSON Web Token (JWT) for the specified application user.
    /// </summary>
    /// <param name="user">The user for whom the JWT will be generated. Must not be null.</param>
    /// <returns>A string containing the generated JWT for the specified user.</returns>
    string GenerateJwtToken(ApplicationUser user);
    /// <summary>
    /// Generates a new refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the refresh token. Cannot be null.</param>
    /// <returns>A string containing the newly generated refresh token.</returns>
    string GenerateRefreshToken(ApplicationUser user);
}
