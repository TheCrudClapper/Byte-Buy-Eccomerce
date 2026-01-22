using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Image;

public record ExistingImageUpdateRequest(
    [Required] Guid Id,
    [MaxLength(50)] string? AltText,
    bool IsDeleted
    );
