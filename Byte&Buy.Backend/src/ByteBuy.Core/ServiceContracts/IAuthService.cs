using ByteBuy.Core.DTO.Auth;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;


/// <summary>
/// Defines methods for authenticating users and managing user registration within the application.
/// </summary>
/// <remarks>Implementations of this interface provide authentication and registration functionality, typically
/// used to support user login and account creation workflows. Methods are asynchronous and support cancellation via a
/// CancellationToken.</remarks>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user with the provided credentials and returns a token response if successful.
    /// </summary>
    /// <param name="request">The login request containing the user's credentials and any additional authentication parameters. Cannot be
    /// null.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the login operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see
    /// cref="Result{TokenResponse}"/> indicating the outcome of the authentication attempt. If successful, the result
    /// includes the token response; otherwise, it contains error information.</returns>
    Task<Result<TokenResponse>> Login(LoginRequest request, CancellationToken cancelationToken = default);
    
    /// <summary>
    /// Registers a new portal user with the specified registration details.
    /// </summary>
    /// <param name="request">The registration information for the new user. Cannot be null.</param>
    /// <param name="cancelationToken">A cancellation token that can be used to cancel the registration operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/> indicating
    /// the outcome of the registration.</returns>
    Task<Result> RegisterPortalUser(RegisterRequest request, CancellationToken cancelationToken = default);
}
