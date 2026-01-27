using Microsoft.AspNetCore.Http;

namespace ByteBuy.Core.DTO.Public.Abstractions;

public interface IImageRequestDto
{
    string? AltText { get; }
    IFormFile Image { get; }
}
