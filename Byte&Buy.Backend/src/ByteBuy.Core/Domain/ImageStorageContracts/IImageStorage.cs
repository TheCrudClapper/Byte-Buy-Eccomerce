using ByteBuy.Core.Domain.ImageStorageContracts.Enums;
using Microsoft.AspNetCore.Http;

namespace ByteBuy.Core.Domain.ImageStorageContracts;

public interface IImageStorage
{
    Task<List<string>> SaveToDirectoryAsync(IReadOnlyList<IFormFile> files, ImageTypeEnum type);
    Task DeleteFromDirectoryAsync(string path, ImageTypeEnum type);
}
