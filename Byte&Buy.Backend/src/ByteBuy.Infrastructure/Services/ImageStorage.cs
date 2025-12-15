using ByteBuy.Core.Domain.ImageStorageContracts;
using ByteBuy.Core.Domain.ImageStorageContracts.Enums;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ByteBuy.Infrastructure.Services;

public class ImageStorage : IImageStorage
{
    private readonly IWebHostEnvironment _env;
    public ImageStorage(IWebHostEnvironment env)
    {
        _env = env;
    }

    private string GetCombinedPath(ImageTypeEnum type)
    {
        var root = Path.Combine(_env.WebRootPath, "Images");

        return type switch
        {
            ImageTypeEnum.Items => Path.Combine(root, "Items"),
            ImageTypeEnum.Employees => Path.Combine(root, "Employees"),
            _ => root,
        };
    }

    public Task DeleteFromDirectoryAsync(string path, ImageTypeEnum type)
    {
        throw new NotImplementedException();
    }

    public Result ValidateExtensions(IReadOnlyList<IFormFile> files)
    {
        string[] allowedExtensions = [".jpg", ".jpeg", ".png"];

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension) || !allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                return Result.Failure(ImageErrors.WrongImageExtensions);
        }

        return Result.Success();
    }

    public async Task<Result<List<string>>> SaveToDirectoryAsync(IReadOnlyList<IFormFile> files, ImageTypeEnum type)
    {
        var validationResult = ValidateExtensions(files);
        if (validationResult.IsFailure)
            return Result.Failure<List<string>>(validationResult.Error);

        var basePath = GetCombinedPath(type);
        Directory.CreateDirectory(basePath);

        var paths = new List<string>();

        foreach (var file in files)
        {
            var ext = Path.GetExtension(file.FileName);

            //eg. 278295A7-0407-4063-AC74-6C366F0786C9.jpg
            var fileName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(basePath, fileName);

            await using var stream = file.OpenReadStream();
            await using var fs = File.Create(fullPath);
            await stream.CopyToAsync(fs);

            paths.Add(Path.Combine(type.ToString(), fileName).Replace("\\", "/"));
        }

        return paths;
    }

}
