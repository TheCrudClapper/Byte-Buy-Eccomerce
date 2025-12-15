using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.Image;

public record ExistingImageUpdateRequest(
    [Required] Guid Id,
    [Required, MaxLength(50)] string AltText,
    bool isDeleted
    );
