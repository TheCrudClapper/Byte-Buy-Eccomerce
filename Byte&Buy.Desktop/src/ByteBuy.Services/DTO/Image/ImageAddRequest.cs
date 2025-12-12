using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.Image;

public record ImageAddRequest(
    [Required, MaxLength(50)] string AltText,
    [Required] IFormFile Image
    );
