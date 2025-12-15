namespace ByteBuy.Services.DTO.Image;

public record ImageAddRequest(
    string AltText,
    string FileName,
    byte[] FileBytes
    );
