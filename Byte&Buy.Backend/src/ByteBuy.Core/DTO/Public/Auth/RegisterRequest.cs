using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Auth;
/// <summary>
/// Represents a request to register a new user with the required personal and account information.
/// </summary>
/// <param name="FirstName">The first name of the user. Must not be null and cannot exceed 50 characters.</param>
/// <param name="LastName">The last name of the user. Must not be null and cannot exceed 50 characters.</param>
/// <param name="Email">The email address of the user. Must not be null.</param>
/// <param name="Password">The password for the user's account. Must not be null.</param>
public record RegisterRequest(
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    [Required] string Password);