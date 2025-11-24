namespace ByteBuy.Services.DTO.Auth;

public record PasswordChangeRequest(
    string NewPassword,
    string CurrentPassword,
    string ConfirmPassword
);

