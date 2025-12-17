using Microsoft.AspNetCore.Http;

namespace ByteBuy.Core.DTO.Abstractions;

public interface IImageRequestDto
{
    string AltText { get; }
    IFormFile Image { get; }
}
