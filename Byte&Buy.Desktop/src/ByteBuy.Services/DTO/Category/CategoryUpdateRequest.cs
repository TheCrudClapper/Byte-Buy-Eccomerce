using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.Category;

public record CategoryUpdateRequest(
    string Name,
    string? Description
    );
