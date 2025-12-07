using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Auth;

/// <summary>
/// Represents a request to authenticate a user using an email address and password.
/// </summary>
/// <param name="Email">The email address associated with the user account. This value is required and cannot be null or empty.</param>
/// <param name="Password">The password for the user account. This value is required and cannot be null or empty.</param>
public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password);