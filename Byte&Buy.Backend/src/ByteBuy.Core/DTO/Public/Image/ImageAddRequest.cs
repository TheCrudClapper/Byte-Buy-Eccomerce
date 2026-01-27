using ByteBuy.Core.DTO.Public.Abstractions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Image;

public record ImageAddRequest(
    [MaxLength(50)] string? AltText,
    [Required] IFormFile Image) : IImageRequestDto;
