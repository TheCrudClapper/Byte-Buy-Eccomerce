using ByteBuy.Core.DTO.Abstractions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Image;

public record ImageAddRequest(
    [MaxLength(50)] string? AltText,
    [Required] IFormFile Image)
    : IImageRequestDto;
