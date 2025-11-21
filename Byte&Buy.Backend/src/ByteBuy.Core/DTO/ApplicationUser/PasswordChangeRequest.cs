using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.ApplicationUser;
public record PasswordChangeRequest(
    [Required, MinLength(8)] string NewPassword,
    [Required, MinLength(8)] string CurrentPassword,
    [Required, MinLength(8)] string ConfirmPassword
    );

