using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Abstractions;

public interface IImageRequestDto
{
    string AltText { get; }
    IFormFile Image { get; }
}
