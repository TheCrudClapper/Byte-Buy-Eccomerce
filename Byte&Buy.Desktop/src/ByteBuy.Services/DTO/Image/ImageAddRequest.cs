using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.Image;

public record ImageAddRequest(
    string AltText,
    IFormFile Image
    );
