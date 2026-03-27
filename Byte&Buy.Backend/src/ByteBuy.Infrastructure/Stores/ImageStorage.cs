using ByteBuy.Core.Contracts;
using ByteBuy.Core.Contracts.Enums;
using ByteBuy.Core.Domain.Items.Errors;
using ByteBuy.Core.Domain.Shared.ResultTypes;
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
            ImageTypeEnum.CompanyLogo => Path.Combine(root, "CompanyLogo"),
            ImageTypeEnum.PortalUsers => Path.Combine(root, "PortalUsers"),
            _ => root,
        };
    }

    public Result DeleteFromDirectory(IEnumerable<string> imagePaths, ImageTypeEnum type)
    {
        try
        {
            foreach (var imagePath in imagePaths)
            {
                var basePath = GetCombinedPath(type);
                var fullPath = Path.Combine(basePath, Path.GetFileName(imagePath));
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }
        }
        catch (UnauthorizedAccessException)
        {
            return Result.Failure(ImageErrors.UnauthorizedAccess);
        }
        catch (IOException)
        {
            return Result.Failure(ImageErrors.StorageFailure);
        }

        return Result.Success();
    }

    public async Task<Result<List<string>>> SaveToDirectoryAsync(IReadOnlyList<IFormFile> files, ImageTypeEnum type)
    {
        var validationResult = ValidateExtensions(files);
        if (validationResult.IsFailure)
            return Result.Failure<List<string>>(validationResult.Error);

        var basePath = GetCombinedPath(type);
        var paths = new List<string>();

        try
        {
            Directory.CreateDirectory(basePath);

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
        }
        catch (UnauthorizedAccessException)
        {
            CleanupFiles(basePath, paths);
            return Result.Failure<List<string>>(ImageErrors.UnauthorizedAccess);
        }
        catch (IOException)
        {
            CleanupFiles(basePath, paths);
            return Result.Failure<List<string>>(ImageErrors.StorageFailure);
        }

        return paths;
    }

    private static void CleanupFiles(string basePath, IEnumerable<string> paths)
    {
        foreach (var relativePath in paths)
        {
            var fullPath = Path.Combine(basePath, Path.GetFileName(relativePath));

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }

    public void RollbackSavedFiles(IEnumerable<string> paths, ImageTypeEnum type)
    {
        var basePath = GetCombinedPath(type);
        foreach (var path in paths)
        {
            var fullPath = Path.Combine(basePath, Path.GetFileName(path));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }

    private static Result ValidateExtensions(IReadOnlyList<IFormFile> files)
    {
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension) || !allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                return Result.Failure(ImageErrors.WrongImageExtensions);
        }

        return Result.Success();
    }

}
