using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Http;

namespace ByteBuy.Core.Contracts;

public interface IImageStorage
{
    Task<Result<List<string>>> SaveToDirectoryAsync(IReadOnlyList<IFormFile> files, ImageTypeEnum type);
    Result DeleteFromDirectory(IReadOnlyList<string> imagePaths);
}
